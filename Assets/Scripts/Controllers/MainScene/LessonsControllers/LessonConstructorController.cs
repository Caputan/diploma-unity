using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Coroutine;
using Diploma.Controllers;
using Diploma.Controllers.AssembleController;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using Diploma.UI;
using GameObjectCreating;
using Interfaces;
using ListOfLessons;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Types = Diploma.Tables.Types;

namespace Controllers
{
    public class LessonConstructorController: IInitialization,INeedScreenShoot,ICleanData
    {
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLessons _gameContextWithLessons;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly FileManagerController _fileManagerController;
        private string[] _destination = new string[5];
        private readonly FileManager _fileManager;
        private readonly AssemblyCreator _assemblyCreator;
        private readonly Transform _assemblyParent;
        private Dictionary<LoadingParts, GameObject> _texts;
        private PlateWithButtonForLessonsFactory _plateWithButtonForLessonsFactory;
        private string[] _localText = new string[4];
        private string[] _localBufferText = new string[4];
        private string[] _massForCopy = new string[3];
        private ErrorCodes _error;
        private GameObject _main;
        private string[] lessonPacked = new string[7];
        private int id;
        public bool _playerChoose;
        private AssemblyInitialization _assemblyInitialization;
        private string _order;
        public event Action<GameObject> GiveMeGameObject;
        
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/LessonPrefab"};
        
        public LessonConstructorController(
            DataBaseController dataBaseController,
            List<IDataBase>  tables,
            GameContextWithViews gameContextWithViews,
            GameContextWithLessons gameContextWithLessons,
            GameContextWithUI gameContextWithUI,
            GameContextWithLogic gameContextWithLogic,
            FileManagerController fileManagerController,
            FileManager fileManager,
            AssemblyCreator assemblyCreator,
            Transform assemblyParent
        )
        {
            _dataBaseController = dataBaseController;
            _tables = tables;
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLessons = gameContextWithLessons;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithLogic = gameContextWithLogic;
            _fileManagerController = fileManagerController;
            _fileManager = fileManager;
            _assemblyCreator = assemblyCreator;
            _assemblyParent = assemblyParent;
            _texts = _gameContextWithViews.TextBoxesOnConstructor;
            _plateWithButtonForLessonsFactory = new PlateWithButtonForLessonsFactory(ResourceLoader.LoadPrefab(_viewPath));
        }

        public void Initialization()
        {
            _fileManagerController.newText += SetTextInTextBox;
            _assemblyCreator.EndCreatingEvent += SavingAssemblyDis;
        }

        public void CleanData()
        {
            _fileManagerController.newText -= SetTextInTextBox;
            _assemblyCreator.EndCreatingEvent -= SavingAssemblyDis;
        }
        
        public void SavingAssemblyDis(string obj)
        {
            _playerChoose = true;
            _order = obj;
        }

        public void SetTextInTextBox(LoadingParts loadingParts, string text,string firstPath)
        {
            switch (loadingParts)
            {
                case LoadingParts.DownloadModel:
                    _localText[0] = text;
                    _texts[loadingParts].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        _localText[0].Split('\\').Last();
                    _massForCopy[0] = firstPath;
                    break;
                case LoadingParts.DownloadVideo:
                    _localText[2] =  text;
                    _texts[loadingParts].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        _localText[2].Split('\\').Last();
                    _massForCopy[2] = firstPath;
                    break;
                case LoadingParts.DownloadPDF:
                    _localText[1] = text;
                    _texts[loadingParts].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        _localText[1].Split('\\').Last();
                    _massForCopy[1] = firstPath;
                    break;
                    // destinationPath[0] = fileManager.CreateFileFolder("Assemblies");
                    // destinationPath[1] = fileManager.CreateFileFolder("Videos");
                    // destinationPath[2] = fileManager.CreateFileFolder("Photos");
                    // destinationPath[3] = fileManager.CreateFileFolder("Texts");
            }
        }
        public ErrorCodes CheckForErrors()
        {
            return _error;
        }

        public void OpenAnUIInitialization()
        {
            OpenAnUIInitializationCoroutine().StartCoroutine(out _, out _);
        }
        
        public IEnumerator OpenAnUIInitializationCoroutine()
        {
            _playerChoose = false;
            _dataBaseController.SetTable(_tables[1]);
            if (_dataBaseController.GetDataFromTable<Lessons>().Count == 0)
            {
                id = 0;
            }
            else
            {
                id = _dataBaseController.GetDataFromTable<Lessons>().Last().Lesson_Id+1;
            }
            _destination[0] = _fileManager.CreateFileFolder(id +"\\"+ "Assemblies");
            _destination[1] = _fileManager.CreateFileFolder(id +"\\"+ "Videos");
            _destination[2] = _fileManager.CreateFileFolder(id +"\\"+ "Photos");
            _destination[3] = _fileManager.CreateFileFolder(id +"\\"+ "Texts");
            _destination[4] = _fileManager.CreateFileFolder(_destination[2] + "\\" + "Parts");
            if (_localText[0] != @"Выберите UnityBundle ()" && File.Exists(_massForCopy[0]))
            {
                _localBufferText[0] =_destination[0]+ "\\" +_localText[0].Split('\\').Last();
                Debug.Log(_localBufferText[0]);
                
                File.Copy(_massForCopy[0],_localBufferText[0],true);
                var GameObjectInitilization = new GameObjectInitialization(id +"\\"+ "Assemblies"+ "\\" +_localText[0].Split('\\').Last(), _fileManager);//id +"\\"+ "Assemblies"+ "\\" +_localText[0], _fileManager);
                GameObjectInitilization.InstantiateGameObject();
                yield return new WaitUntil(() => GameObjectInitilization.GameObject != null);
                _main = GameObjectInitilization.GameObject;
                _assemblyInitialization = new AssemblyInitialization(_main,_assemblyParent);
                _assemblyInitialization.Initialization();
                _error = ErrorCodes.None;
                GiveMeGameObject.Invoke(_assemblyInitialization.GetAGameObject());
            }
            else
            {
                _error = ErrorCodes.FileDoesNotExist;
                GiveMeGameObject.Invoke(new GameObject());
            }
        }

        public void DestroyAssembly()
        {
            Object.Destroy(_assemblyInitialization.GetAGameObject());
        }
        public bool CreateALesson()
        {
             //это из меню
             // 0 - assembles
             // 1 - lessons
             // 2 - text
             // 3 - types
             // 4 - users
             // 5 - videos
             //порядок заполнения в бд таблицы Lessons
             // 0 - preview
             // 1 - text
             // 2 - video
             // 3 - assembly
             // 4 - type
             // 5 - name
            
             lessonPacked[0] = null;
             lessonPacked[1] = null;
             lessonPacked[2] = null;
             lessonPacked[3] = null;
             lessonPacked[4] = null;
             lessonPacked[5] = null;
             lessonPacked[6] = null;
             // creating Folder-packed
             // нужно добавить id
             _dataBaseController.SetTable(_tables[1]);
             
             
            
            // add new assembly
            if (_localText[0] != @"Выберите UnityBundle ()"&& _error == ErrorCodes.None && File.Exists(_massForCopy[0]))
            {
                _localBufferText[0] =_destination[0]+ "\\" +_localText[0].Split('\\').Last();
                Debug.Log(_localBufferText[0]);
                File.Copy(_massForCopy[0],_localBufferText[0],true);
                _dataBaseController.SetTable(_tables[0]);
                string[] assemblyPacked = new string[1];
                assemblyPacked[0] = id +"\\"+ "Assemblies"+ "\\" +_localText[0].Split('\\').Last();
                _dataBaseController.AddNewRecordToTable(assemblyPacked);
                lessonPacked[3] = _dataBaseController.GetDataFromTable<Assemblies>().Last().Assembly_Id.ToString();
            }
            else
            {
                _error = ErrorCodes.FileDoesNotExist;
            }

            // add new text
            if (_localText[1] != @"Выберите текстовый фаил(*.pdf)"&& _error == ErrorCodes.None
                                                                  && File.Exists(_massForCopy[1]))
            {
                _localBufferText[1] = _destination[3] + "\\" + _localText[1].Split('\\').Last();
                Debug.Log(_localBufferText[1]);
                if (_massForCopy[1] != "")
                    //if (!File.Exists(_localBufferText[1]))
                    File.Copy(_massForCopy[1], _localBufferText[1],true);
                _dataBaseController.SetTable(_tables[2]);
                string[] textPacked = new string[1];
                textPacked[0] = id +"\\"+ "Texts"+ "\\" +_localText[1].Split('\\').Last();
                _dataBaseController.AddNewRecordToTable(textPacked);
                lessonPacked[1] = _dataBaseController.GetDataFromTable<Texts>().Last().Text_Id.ToString();  
                //PDFReader pdfReader = new PDFReader();
                //pdfReader.ProcessRequest(textPacked[0],_destination[2]);
            }
            else
            {
                _error = ErrorCodes.FileDoesNotExist;
            }

            // add new video
            if (_localText[2] != @"Выберите видео-фаил (*.mp4)"&& _error == ErrorCodes.None
                                                               && File.Exists(_massForCopy[2]))
            {
                Debug.Log(_localBufferText[2]);
                _localBufferText[2] = _destination[1]+ "\\" +  _localText[2].Split('\\').Last();
                if(_massForCopy[2]!="")
                    File.Copy(_massForCopy[2],_localBufferText[2],true);
                _dataBaseController.SetTable(_tables[5]);
                string[] videoPacked = new string[1];
                videoPacked[0] = id +"\\"+ "Videos"+ "\\" +_localText[2].Split('\\').Last();
                _dataBaseController.AddNewRecordToTable(videoPacked);
                lessonPacked[2] = _dataBaseController.GetDataFromTable<Videos>().Last().Video_Id.ToString();
            }
            //
            
            // add name
            if (_texts[LoadingParts.SetNameToLesson].GetComponent<TMP_InputField>().text != "")
            {
                lessonPacked[5] = _texts[LoadingParts.SetNameToLesson].
                    GetComponent<TMP_InputField>().text;
            }
            else
            {
                Debug.Log("EmptyName");
                _error = ErrorCodes.EmptyInputError;
            }


            
            
            int count = 0;
            _dataBaseController.SetTable(_tables[3]);
            string[] typePacked = new string[1];
            typePacked[0] = null;
            foreach (var key in _gameContextWithViews.ChoosenToggles.Values) {
                if (key.GetComponentInChildren<Toggle>().isOn)
                {
                    typePacked[0] += count + ",";
                }
                count++;
            }

            if (typePacked[0] == null)
            {
                Debug.Log("EmptyLibrary");
                _error = ErrorCodes.EmptyInputError;
            }
            _dataBaseController.AddNewRecordToTable(typePacked);
            lessonPacked[4] = _dataBaseController.GetDataFromTable<Types>().Last().Type_Id.ToString();
            if (CheckForErrors()!= ErrorCodes.None)
            {
                DeleteNotUsingLesson(lessonPacked);
                for (int i = 0; i < 6; i++)
                {
                    lessonPacked[i] = null;
                }
                return false;
            }
            
            //Screens of parts

            var parts = _assemblyInitialization?.GetAGameObject().GetComponentsInChildren<MeshRenderer>();
            if (parts != null)
            {
                foreach (var meshRenderer in parts)
                {
                    meshRenderer.enabled = false;
                }
            }
            else
            {
                _error = ErrorCodes.EmptyInputError;
            }

            if (_error != ErrorCodes.None)
            {
                return false;
            }
            TakingScreensOfParts(parts,lessonPacked,id);

            return true;
            
        }

        private void TakingScreensOfParts(
            MeshRenderer[] meshRenderers,
            string[] lessonPacked,
            int id
        )
        {
            int i = 0;
            ScreenShotsCoroutine(meshRenderers,i,lessonPacked,id).StartCoroutine(out _,out _);
        }
        
        private IEnumerator ScreenShotsCoroutine(
            MeshRenderer[] meshRenderer, 
            int i,
            string[] lessonPacked,
            int assemblyId)
        {
            if (i >= meshRenderer.Length)
            {
                // add screens
                foreach (var mesh in meshRenderer)
                {
                    mesh.enabled = true;
                }
                SetCameraNearObject(_assemblyInitialization.GetAGameObject(),_gameContextWithLogic.MainCamera);
                yield return new WaitForFixedUpdate();
                TakingScreen(_destination[2]+"\\"+lessonPacked[5]+".png");
                lessonPacked[0] = assemblyId +"\\"+ "Photos"+  "\\" + lessonPacked[5]+".png";
                yield return new WaitUntil(() => _playerChoose == true);
                lessonPacked[6] = _order;
                if (lessonPacked[0] == null|| lessonPacked[0] == "")
                {
                    _error = ErrorCodes.EmptyInputError;
                }
                if (lessonPacked[1] == null|| lessonPacked[1] == "")
                {
                    _error = ErrorCodes.EmptyInputError;
                }
                if (lessonPacked[3] == null|| lessonPacked[3] == "")
                {
                    _error = ErrorCodes.EmptyInputError;
                }
                if (lessonPacked[4] == null|| lessonPacked[4] == "")
                {
                    _error = ErrorCodes.EmptyInputError;
                }
                if (lessonPacked[6] == null|| lessonPacked[6] == "")
                {
                    _error = ErrorCodes.EmptyInputError;
                }
                if (_error == ErrorCodes.None)
                {
                    _dataBaseController.SetTable(_tables[1]);
                    _dataBaseController.AddNewRecordToTable(lessonPacked);
                }
                else
                {
                    DeleteNotUsingLesson(lessonPacked);
                    Debug.Log("Deleting BullShit");
                    for (int g = 0; g < 6; g++)
                    {
                        lessonPacked[g] = null;
                    }
                }
                yield break;
            }
            
            meshRenderer[i].enabled = true;
            yield return new WaitForFixedUpdate();
            SetCameraNearObject(meshRenderer[i].gameObject,_gameContextWithLogic.ScreenShotCamera);
            Debug.Log(_destination[4]+"\\PartNumber"+i+".png");
            TakingScreenShotOfPart(_destination[4]+"\\PartNumber"+i+".png");
            yield return new WaitForSeconds(0.1f);
            meshRenderer[i].enabled = false;
            i++;
            ScreenShotsCoroutine(meshRenderer,i,lessonPacked,assemblyId).StartCoroutine(out _,out _);
            yield break;
        }
        public void SetCameraNearObject(GameObject gameObject, Camera witchCamera)
        {
            //gameObject.transform.position = gameObject.transform.position.Change(x:0f,y: 0f,z: 0f);
            witchCamera.transform.LookAt(gameObject.transform);
        }
        private void DeleteNotUsingLesson(string[] lessonPacked)
        {
            
            if (lessonPacked[1]!=null)
            {
                _dataBaseController.SetTable(_tables[2]);
                _dataBaseController.DeleteLastRecord(_dataBaseController.GetDataFromTable<Texts>().Last().Text_Id);
            }
            if (lessonPacked[2]!=null)
            {
                _dataBaseController.SetTable(_tables[5]);
                _dataBaseController.DeleteLastRecord(_dataBaseController.GetDataFromTable<Videos>().Last().Video_Id);
            }
            if (lessonPacked[3]!=null)
            {
                _dataBaseController.SetTable(_tables[0]);
                _dataBaseController.DeleteLastRecord(_dataBaseController.GetDataFromTable<Assemblies>().Last().Assembly_Id);
            }
            if (lessonPacked[4]!=null)
            {
                _dataBaseController.SetTable(_tables[3]);
                _dataBaseController.DeleteLastRecord(_dataBaseController.GetDataFromTable<Types>().Last().Type_Id);
            }
        }

        

        public void AddNewLessonToListOnUI()
        {
            _dataBaseController.SetTable(_tables[1]);
            
            Lessons lastLessonInDb = _dataBaseController.GetDataFromTable<Lessons>().Last();
            
            var lessonToggle = _plateWithButtonForLessonsFactory.Create(
                _gameContextWithUI.UiControllers[LoadingParts.LoadLectures].transform.GetChild(2).GetChild(0).GetChild(0).gameObject.transform);
            lessonToggle.transform.localPosition = new Vector3(0,0,0);
            
            var tex = new Texture2D(5, 5);
            tex.LoadImage(File.ReadAllBytes(_fileManager.GetStorage() +"\\"+ lastLessonInDb.Lesson_Preview));
            lessonToggle.GetComponentInChildren<RawImage>().texture = tex;

            var lessonName = lessonToggle.GetComponentInChildren<TextMeshProUGUI>();
            lessonName.text = lastLessonInDb.Lesson_Name;
                
            _gameContextWithLessons.AddLessonsView(lastLessonInDb.Lesson_Id,
                new ListOfLessonsView(lastLessonInDb.Lesson_Id,
                    lessonToggle));
            _gameContextWithViews.AddLessonsToggles(lastLessonInDb.Lesson_Id,lessonToggle);

            //var lessonChooseButtonsLogic = new LessonChooseButtonsLogic(_gameContextWithViews.ChoosenLessonToggles);
            //lessonChooseButtonsLogic.AddNewButton(lastLessonInDb.Lesson_Id);
            _gameContextWithViews.LessonChooseButtonsLogic.AddNewButton(lastLessonInDb.Lesson_Id);
           
        }
        public event Action<string> TakeScreenShoot;
        
        public event Action<string> TakeScreenShootOfPart;
        public void TakingScreen(string name)
        {
            TakeScreenShoot?.Invoke(name);
        }

        public void TakingScreenShotOfPart(string name)
        {
            TakeScreenShootOfPart?.Invoke(name);
        }
    }
}
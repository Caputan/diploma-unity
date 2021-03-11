using System;
using System.Collections.Generic;
using System.IO;
using Controllers;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;
using ListOfLessons;
using UnityEngine;
using UnityEngine.UI;
using Types = Diploma.Tables.Types;

namespace Diploma.UI
{
    public class GameContexWithViewCreator: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly GameContextWithLessons _gameContextWithLessons;
        private readonly GameObject _canvasParent;
        private GameObject _prefabTogglePanel;
        private readonly GameObject _lessionsParent;
        private readonly GameObject _prefabToggleLessions;
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private TogglePanelFactory _togglePanelFactory;
        private TogglePanelFactory _toggleLessionsFactory;
        private FactoryType _factoryType;
        
        public GameContexWithViewCreator(GameContextWithViews gameContextWithViews,
            GameContextWithLogic gameContextWithLogic,
            GameContextWithLessons gameContextWithLessons,
            GameObject CanvasParent,GameObject PrefabTogglePanel,
            GameObject LessionsParent,GameObject PrefabToggleLessions,
            DataBaseController dataBaseController,List<IDataBase> tables)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLogic = gameContextWithLogic;
            _gameContextWithLessons = gameContextWithLessons;
            _canvasParent = CanvasParent;
            _prefabTogglePanel = PrefabTogglePanel;
            _lessionsParent = LessionsParent;
            _prefabToggleLessions = PrefabToggleLessions;
            _dataBaseController = dataBaseController;
            _tables = tables;
            _togglePanelFactory = new TogglePanelFactory(_prefabTogglePanel,_canvasParent.GetComponent<ToggleGroup>());
            _toggleLessionsFactory = new TogglePanelFactory(_prefabToggleLessions,_lessionsParent.GetComponent<ToggleGroup>());
        }

        public void Initialization()
        {
            _dataBaseController.SetTable(_tables[3]);
            List<Types> dataTypesFromTable = _dataBaseController.GetDataFromTable<Types>();
            int index = 0;
            foreach (FactoryType factoryType in Enum.GetValues(typeof(FactoryType)))
            {
                var toggle = _togglePanelFactory.Create(_canvasParent.transform);
                toggle.transform.localPosition = new Vector3(0,0,0);
                var tex = new Texture2D(5, 5);
                tex.LoadImage(File.ReadAllBytes(dataTypesFromTable[index].Type_Image));
                toggle.GetComponentInChildren<RawImage>().texture = tex;
                _gameContextWithLogic.AddFactoryTypeForCreating(toggle.GetInstanceID(),factoryType);
                _gameContextWithViews.AddToggles(toggle.GetInstanceID(),toggle);
                index++;
            }

            _dataBaseController.SetTable(_tables[1]);
            List<Lessons> dataLessonsFromTable = _dataBaseController.GetDataFromTable<Lessons>();
            
            foreach (var lesson in dataLessonsFromTable)
            {
                var lessonToggle = _toggleLessionsFactory.Create(_lessionsParent.transform);
                lessonToggle.transform.localPosition = new Vector3(0,0,0);
                var tex = new Texture2D(5, 5);
                tex.LoadImage(File.ReadAllBytes(lesson.Lesson_Preview));
                lessonToggle.GetComponentInChildren<RawImage>().texture = tex;
                
                _gameContextWithLessons.AddLessonsView(lesson.Lesson_Id,
                    new ListOfLessonsView(lesson.Lesson_Id,
                        lessonToggle));
                _gameContextWithViews.AddLessonsToggles(lesson.Lesson_Id,lessonToggle);
            }
            
        }
    }
}
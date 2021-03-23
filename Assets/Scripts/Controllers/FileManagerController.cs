using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Coroutine;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using SimpleFileBrowser;


namespace Diploma.Controllers
{
    public sealed class FileManagerController: IInitialization, IDataBaseFileManager
    {
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly string[] _destinationPath;
        public DataBaseController DataBaseController;
        public List<IDataBase> Tables;
        public Loader3DS Loader3Ds;
        
        

        public FileManagerController(GameContextWithUI gameContextWithUI,string[] destinationPath)
        {
            _gameContextWithUI = gameContextWithUI;
            _destinationPath = destinationPath;
            FileBrowser.SetFilters(true, new FileBrowser.Filter("Assemblies", ".3ds"),
                new FileBrowser.Filter("Text Files", ".doc", ".pdf", ".docx"),
                new FileBrowser.Filter("Videos", ".mp4"));

            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        }

        public void Initialization() { }
        public event Action<LoadingParts,string> newText;

        public void ShowNewText(LoadingParts loadingParts,string text)
        {
            newText.Invoke(loadingParts,text);
        }

        public void ShowLoadDialog()
        {
            ShowLoadDialogCoroutine().StartCoroutine(out _);
        }

        public void ShowSaveDialog(FileTypes fileTypes)
        {
            ShowSaveDialogCoroutine(fileTypes).StartCoroutine(out _);
        }


        private IEnumerator ShowLoadDialogCoroutine()
        {
            yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null,
                "Выберите файл для загрузки", "Загрузить");

            if (FileBrowser.Success)
            {
                
            }
        }

        private IEnumerator ShowSaveDialogCoroutine(FileTypes fileTypes)
        {
            yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null,
                "Save Files", "Save");

            // Это надо все перенести на кнопку.....
            
            if (FileBrowser.Success)
            {
                // = Path.Combine(Application.persistentDataPath,
                //    FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
                string[] localPath = new string[1];
                LoadingParts parts;
                switch (fileTypes)
                {
                    case FileTypes.Assembly:
                        localPath[0] = Path.Combine(_destinationPath[0],
                            FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
                        parts = LoadingParts.DownloadModel;
                        // FileBrowserHelpers.CopyFile(FileBrowser.Result[0], localPath[0]);
                        //
                        // DataBaseController.SetTable(Tables[0]);
                        // DataBaseController.AddNewRecordToTable(localPath);
                        break;
                    // case FileTypes.Image:
                    //     localPath[0] = Path.Combine(destinationPath[2],
                    //         FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
                        
                        // FileBrowserHelpers.CopyFile(FileBrowser.Result[0], localPath[0]);
                        //
                        // DataBaseController.SetTable(Tables[3]);
                        // DataBaseController.AddNewRecordToTable(localPath);
                        break;
                    case FileTypes.Text:
                        localPath[0] = Path.Combine(_destinationPath[3],
                            FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
                        parts = LoadingParts.DownloadPDF;
                        // FileBrowserHelpers.CopyFile(FileBrowser.Result[0], localPath[0]);
                        //
                        // DataBaseController.SetTable(Tables[2]);
                        // DataBaseController.AddNewRecordToTable(localPath);
                        break;
                    case FileTypes.Video:
                        localPath[0] = Path.Combine(_destinationPath[1],
                            FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
                        parts = LoadingParts.DownloadVideo;
                        // FileBrowserHelpers.CopyFile(FileBrowser.Result[0], localPath[0]);
                        //
                        // DataBaseController.SetTable(Tables[5]);
                        // DataBaseController.AddNewRecordToTable(localPath);
                        break;
                    // case FileTypes.LessonPreview:
                    //     localPath[0] = Path.Combine(destinationPath[2],
                    //         FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
                    //
                        //FileBrowserHelpers.CopyFile(FileBrowser.Result[0], localPath[0]);
                
                        // DataBaseController.SetTable(Tables[1]);
                        // DataBaseController.AddNewRecordToTable(localPath);
                        break;
                    default:
                        throw new Exception("TAK DELAT NELZYA");
                }
                ShowNewText(parts,localPath[0]);
                
            }
        }
        
        
    }
}
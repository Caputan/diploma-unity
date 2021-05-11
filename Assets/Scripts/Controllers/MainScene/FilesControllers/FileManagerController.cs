using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Controllers;
using Coroutine;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using SimpleFileBrowser;
using UnityEngine;


namespace Diploma.Controllers
{
    public sealed class FileManagerController: IInitialization, IDataBaseFileManager
    {
      
        

        private ErrorCodes _error;
        
        

        public FileManagerController()
        {
            FileBrowser.SetFilters(true,
                new FileBrowser.Filter("Text Files",".pdf"),
                new FileBrowser.Filter("Videos", ".mp4"));

            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
            _error = ErrorCodes.EmptyInputError;
        }

        public void Initialization() { }
        public event Action<LoadingParts,string,string> newText;

        public void ShowNewText(LoadingParts loadingParts, string text,string firstPath)
        {
            newText?.Invoke(loadingParts, text,firstPath);
        }

        public void ShowLoadDialog()
        {
            ShowLoadDialogCoroutine().StartCoroutine(out _,out _);
        }

        public void ShowSaveDialog(FileTypes fileTypes)
        {
            ShowSaveDialogCoroutine(fileTypes).StartCoroutine(out _,out _);
        }

        public ErrorCodes CheckForErrors()
        {
            return _error;
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

            if (FileBrowser.Success)
            {
               
                string[] localPath = new string[1];
                LoadingParts parts;
                
                string splitedString = 
                    FileBrowserHelpers.GetFilename(FileBrowser.Result[0]);
                string[] isFormatTrue;
                switch (fileTypes)
                {
                    case FileTypes.Assembly:
                        //var isFormatTrue = splitedString.Split('\\').Last().Split('.');
                        //if (isFormatTrue.Last() != "3ds")
                        //{
                            //_error = ErrorCodes.WrongFormatError;
                            //splitedString = @"Выберите деталь (*.3ds)";
                        //}
                        //else
                        //{
                        localPath[0] = FileBrowser.Result[0];
                        _error = ErrorCodes.None;
                        //}
                        parts = LoadingParts.DownloadModel;
                        // FileBrowserHelpers.CopyFile(FileBrowser.Result[0], localPath[0]);
                        //
                        // DataBaseController.SetTable(Tables[0]);
                        // DataBaseController.AddNewRecordToTable(localPath);
                        break;
                    case FileTypes.Text:
                        isFormatTrue = splitedString.Split('\\').Last().Split('.');
                        if (isFormatTrue.Last() != "pdf" )
                        {
                            _error = ErrorCodes.WrongFormatError;
                            splitedString = "Выберите текстовый фаил(*.pdf)";
                        }
                        else
                        {
                            localPath[0] = FileBrowser.Result[0];
                            
                            _error = ErrorCodes.None;
                        }
                        parts = LoadingParts.DownloadPDF;
                        // FileBrowserHelpers.CopyFile(FileBrowser.Result[0], localPath[0]);
                        //
                        // DataBaseController.SetTable(Tables[2]);
                        // DataBaseController.AddNewRecordToTable(localPath);
                        break;
                    case FileTypes.Video:
                        isFormatTrue = splitedString.Split('\\').Last().Split('.');
                        if (isFormatTrue.Last() != "mp4")
                        {
                            _error = ErrorCodes.WrongFormatError;
                            splitedString = "Выберите видео-фаил (*.mp4)";
                        }
                        else
                        {
                            localPath[0] = FileBrowser.Result[0];
                            _error = ErrorCodes.None;
                        }
                        parts = LoadingParts.DownloadVideo;
                        // FileBrowserHelpers.CopyFile(FileBrowser.Result[0], localPath[0]);
                        //
                        // DataBaseController.SetTable(Tables[5]);
                        // DataBaseController.AddNewRecordToTable(localPath);
                        break;
                    default:
                        throw new Exception("TAK DELAT NELZYA");
                }
                
                ShowNewText(parts,splitedString,localPath[0]);
                
            }
        }
        
        
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Diploma.Enums;
using Diploma.Interfaces;
using SimpleFileBrowser;
using UnityEngine;

namespace Controllers
{
    public class FileManagerController: MonoBehaviour
    {
        public DataBaseController DataBaseController;
        public List<IDataBase> Tables;
        public void Start()
        {
            FileBrowser.SetFilters( true, new FileBrowser.Filter( "Assemblies", ".3do" ), 
                new FileBrowser.Filter( "Text Files", ".doc", ".pdf", ".docx" ), 
                new FileBrowser.Filter("Videos", ".mp4") );
		
            FileBrowser.SetExcludedExtensions( ".lnk", ".tmp", ".zip", ".rar", ".exe" );
        }
        public void ShowLoadDialog()
        {
            StartCoroutine(ShowLoadDialogCoroutine());
        }

        public void ShowSaveDialog(FileTypes fileTypes)
        {
            StartCoroutine(ShowSaveDialogCoroutine(fileTypes));
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
                string destinationPath = Path.Combine(Application.persistentDataPath,
                    FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
                //FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
                switch (fileTypes)
                {
                    case FileTypes.Assebly:
                        DataBaseController.SetTable(Tables[0]);
                        DataBaseController.AddNewRecordToTable(null,FileBrowser.Result[0]);
                        break;
                    case FileTypes.Image:
                        DataBaseController.SetTable(Tables[3]);
                        DataBaseController.AddNewRecordToTable(null,FileBrowser.Result[0]);
                        break;
                    case FileTypes.Text:
                        DataBaseController.SetTable(Tables[2]);
                        DataBaseController.AddNewRecordToTable(null,FileBrowser.Result[0]);
                        break;
                    case FileTypes.Video:
                        DataBaseController.SetTable(Tables[5]);
                        DataBaseController.AddNewRecordToTable(null,FileBrowser.Result[0]);
                        break;
                }
                
               
                
               
            }
        }
    }
}
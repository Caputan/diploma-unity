using System.Collections;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;

public class FileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		FileBrowser.SetFilters( true, new FileBrowser.Filter( "Assemblies", ".3do" ), 
			new FileBrowser.Filter( "Text Files", ".doc", ".pdf", ".docx" ), 
			new FileBrowser.Filter("Videos", ".mp4") );
		
		FileBrowser.SetExcludedExtensions( ".lnk", ".tmp", ".zip", ".rar", ".exe" );
		

		// FileBrowser.AddQuickLink( "Users", "C:\\Users", null );
	}

    public void ShowLoadDialog()
    {
	    StartCoroutine(ShowLoadDialogCoroutine());
    }

    public void ShowSaveDialog()
    {
	    StartCoroutine(ShowSaveDialogCoroutine());
    }


    private IEnumerator ShowLoadDialogCoroutine()
    {
	    yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null,
		    "Выберите файл для загрузки", "Загрузить");

	    if (FileBrowser.Success)
	    {
		    //Manipulations with file
	    }
    }

    private IEnumerator ShowSaveDialogCoroutine()
    {
	    yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null,
		    "Save Files", "Save");

	    if (FileBrowser.Success)
	    {
		    string destinationPath = Path.Combine(Application.persistentDataPath,
			    FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
		    FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
	    }
    }
}

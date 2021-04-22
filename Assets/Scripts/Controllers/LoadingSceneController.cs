using System.Collections;
using System.Net.Mime;
using Coroutine;
using Diploma.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Controllers
{
    public sealed class LoadingSceneController: IInitialization
    {
        

        public void LoadNextScene(int idScene)
        {
            AsyncLoad(idScene).StartCoroutine(out _);
        }
        
        IEnumerator AsyncLoad(int sceneID)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
            while (!operation.isDone)
            {
                yield return null;
            }

        }
        
        
        public void Initialization()
        {
        }

        
    }
}
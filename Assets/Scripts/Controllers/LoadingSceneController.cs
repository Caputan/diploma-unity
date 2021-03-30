using System.Net.Mime;
using Diploma.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class LoadingSceneController: IInitialization
    {
        public void LoadNextScene(int idScene)
        {
            SceneManager.LoadScene(idScene, LoadSceneMode.Single);
        }
        
        public void Initialization()
        {
            throw new System.NotImplementedException();
        }
    }
}
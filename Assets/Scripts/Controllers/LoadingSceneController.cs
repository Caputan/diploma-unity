using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Coroutine;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.PracticeScene.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Controllers
{
    public sealed class LoadingSceneController
    {
        private List<Scene> ListOfScenes = new List<Scene>();
        private List<GameObject[]> _roots = new List<GameObject[]>();
        private bool firstCallMain, firstCallPractice, firstCallTheory;

        public LoadingSceneController()
        {
            firstCallMain = true;
            firstCallPractice = true;
            firstCallTheory = true;
        }
        
        public void LoadScenes()
        {
            AsyncLoad().StartCoroutine(out _, out _);
        }
        
        IEnumerator AsyncLoad()
        {
            ListOfScenes.Add(SceneManager.GetSceneByBuildIndex(0));
            AsyncOperation operation1 = SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
            ListOfScenes.Add(SceneManager.GetSceneByBuildIndex(1));
            while (!operation1.isDone)
            {
                yield return null;
            }
            AsyncOperation operation2 = SceneManager.LoadSceneAsync(2,LoadSceneMode.Additive);
            ListOfScenes.Add(SceneManager.GetSceneByBuildIndex(2));
            while (!operation2.isDone)
            {
                yield return null;
            }

            yield return new WaitForEndOfFrame();
            _roots.Add(ListOfScenes[0].GetRootGameObjects());
            _roots.Add(ListOfScenes[1].GetRootGameObjects());
            _roots.Add(ListOfScenes[2].GetRootGameObjects());
            SetActiveSceneAndLoadIt(0);
            StopC();
        }

        private void StopC()
        {
            AsyncLoad().StopCoroutine();
        }
        public void SetActiveSceneAndLoadIt(int idScene)
        {
            SceneManager.SetActiveScene(ListOfScenes[idScene]);
            //root 0
                // 0 - game
                // 3 - editor
            //root 1
                // 0 - game
                // 2 - editor
            //root 2
                // 1 - game
                // 0 - editor
            
            #if UNITY_EDITOR
              switch (idScene)
            {
                case 0:
                    _roots[1][2].GetComponent<TheorySceneInitialization>().OnDestroyNotMB(firstCallTheory);
                    _roots[2][0].GetComponent<PracticeSceneInitialization>().OnDestroyNotMB(firstCallPractice);
                    foreach (var root in _roots[0])
                    {
                        root.SetActive(true);
                    }
                    _roots[0][3].GetComponent<MainMenuSceneInitialization>().StartNotMB();
                    foreach (var root in _roots[1])
                    {
                        root.SetActive(false);
                    }
                    foreach (var root in _roots[2])
                    {
                        root.SetActive(false);
                    }
                    firstCallMain = false;
                    break;
                case 1:
                    _roots[0][3].GetComponent<MainMenuSceneInitialization>().OnDestroyNotMB(firstCallMain);
                    _roots[2][0].GetComponent<PracticeSceneInitialization>().OnDestroyNotMB(firstCallPractice);
                    foreach (var root in _roots[0])
                    {
                        root.SetActive(false);
                    }
                    foreach (var root in _roots[1])
                    {
                        root.SetActive(true);
                    }
                    _roots[1][2].GetComponent<TheorySceneInitialization>().StartNotMB();
                    foreach (var root in _roots[2])
                    {
                        root.SetActive(false);
                    }
                    firstCallTheory = false;
                    break;
                case 2:
                    _roots[0][3].GetComponent<MainMenuSceneInitialization>().OnDestroyNotMB(firstCallMain);
                    _roots[1][2].GetComponent<TheorySceneInitialization>().OnDestroyNotMB(firstCallTheory);
                    foreach (var root in _roots[0])
                    {
                        root.SetActive(false);
                    }
                    foreach (var root in _roots[1])
                    {
                        root.SetActive(false);
                    }
                    foreach (var root in _roots[2])
                    {
                        root.SetActive(true);
                    }

                    if (firstCallPractice)
                    {
                        _roots[2][0].GetComponent<PracticeSceneInitialization>().FirstStartOfStart();
                    }
                    else
                    {
                        _roots[2][0].GetComponent<PracticeSceneInitialization>().DecompileGameScene(false);
                    }
                    firstCallPractice = false;
                    break;
            }
        
            #else
            switch (idScene)
            {
                case 0:
                    _roots[1][0].GetComponent<TheorySceneInitialization>().OnDestroyNotMB(firstCallTheory);
                    _roots[2][1].GetComponent<PracticeSceneInitialization>().OnDestroyNotMB(firstCallPractice);
                    foreach (var root in _roots[0])
                    {
                        root.SetActive(true);
                    }
                    _roots[0][0].GetComponent<MainMenuSceneInitialization>().StartNotMB();
                    foreach (var root in _roots[1])
                    {
                        root.SetActive(false);
                    }
                    foreach (var root in _roots[2])
                    {
                        root.SetActive(false);
                    }
                    firstCallMain = false;
                    break;
                case 1:
                    _roots[0][0].GetComponent<MainMenuSceneInitialization>().OnDestroyNotMB(firstCallMain);
                    _roots[2][1].GetComponent<PracticeSceneInitialization>().OnDestroyNotMB(firstCallPractice);
                    foreach (var root in _roots[0])
                    {
                        root.SetActive(false);
                    }
                    foreach (var root in _roots[1])
                    {
                        root.SetActive(true);
                    }
                    _roots[1][0].GetComponent<TheorySceneInitialization>().StartNotMB();
                    foreach (var root in _roots[2])
                    {
                        root.SetActive(false);
                    }
                    firstCallTheory = false;
                    break;
                case 2:
                    _roots[0][0].GetComponent<MainMenuSceneInitialization>().OnDestroyNotMB(firstCallMain);
                    _roots[1][0].GetComponent<TheorySceneInitialization>().OnDestroyNotMB(firstCallTheory);
                    foreach (var root in _roots[0])
                    {
                        root.SetActive(false);
                    }
                    foreach (var root in _roots[1])
                    {
                        root.SetActive(false);
                    }
                    foreach (var root in _roots[2])
                    {
                        root.SetActive(true);
                    }

                    if (firstCallPractice)
                    {
                        _roots[2][1].GetComponent<PracticeSceneInitialization>().FirstStartOfStart();
                    }
                    else
                    {
                        _roots[2][1].GetComponent<PracticeSceneInitialization>().DecompileGameScene(false);
                    }
                    firstCallPractice = false;
                    break;
            }
            #endif
            
        }
    }
}
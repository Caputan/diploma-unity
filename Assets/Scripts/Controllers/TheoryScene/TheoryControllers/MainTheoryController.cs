using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coroutine;
using Diploma.Enums;
using iTextSharp.text;
using PDFWorker;
using UI.LoadingUI;
using UnityEngine;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class MainTheoryController
    {
        private readonly TheoryController _theoryController;
        private readonly LibraryTreeController _libraryTreeController;
        private readonly LoadingUILogic _loadingUILogic;
        private Dictionary<int, int> _corutineQueue;
        private Dictionary<int, string> _realQueue;
        private int _countOfUnitInQueue;
        private int _hand;
        private List<IEnumerator> _coroutineController;
        private List<CoroutineController> _states;
        private IEnumerator _coroutine;
        private CoroutineController _state;
        
        
        public MainTheoryController(
            TheoryController theoryController, 
            LibraryTreeController libraryTreeController,
            LoadingUILogic loadingUILogic
             )
        {
            _theoryController = theoryController;
            _libraryTreeController = libraryTreeController;
            _loadingUILogic = loadingUILogic;
            _corutineQueue = new Dictionary<int, int>();
            _realQueue = new Dictionary<int, string>();
            _coroutineController = new List<IEnumerator>();
            _states = new List<CoroutineController>();
            _countOfUnitInQueue = 0;
            _hand = 0;
            _theoryController.rewriteDictionary += CreateQueue;
            _libraryTreeController.rewriteDictionary += CreateQueue;
        }

        private void CreateQueue(Dictionary<int,string> controllerInfo)
        {
            foreach (var info in controllerInfo)
            {
                _realQueue.Add(info.Key,info.Value);
                _corutineQueue.Add(_countOfUnitInQueue,info.Key);
                _countOfUnitInQueue++;
            }
            Debug.Log("Creating Queue");
        }
        
        public void StartDoingSomeThingWithQueue()
        {
            
            QueueCorutine(_hand).StartCoroutine(out _coroutine, out _);
            _coroutineController.Add(_coroutine);
            //надо подумать над переводом этого метода в корутину,чтобы останавливать остальные.
            
        }

        private IEnumerator QueueCorutine(int index)
        {
            Debug.Log("Creating New document "+index);
            yield return new WaitForEndOfFrame();
            if (index >= _countOfUnitInQueue)
            {
                _theoryController.LoadDocumentTheory();
                _loadingUILogic.SetActiveLoading(false);
                
                yield break;
            }

            if (index == 0)
            {
                _theoryController.CreateDocumentLocaly(
                    _corutineQueue[_hand],
                    _realQueue[_corutineQueue[_hand]],
                    _states,
                    _hand
                );
                _hand++;
                Debug.Log("я жду создания читалки");
                yield return new WaitUntil(() => _states.Count == _hand);
                Debug.Log("читалка создана, ждем окончания чтения");
                yield return new WaitUntil(() => _states[index].state == CoroutineState.Finished);
                Debug.Log("состояние "+_states[index].state);
                Debug.Log("Начинаем новую короутину");
                //_coroutineController[index].StopCoroutine();
                QueueCorutine(_hand).StartCoroutine(out _coroutine, out _);
                _coroutineController.Add(_coroutine);
            }
            else
            {
               // yield return new WaitUntil(() => _coroutineController.state == CoroutineState.Finished );
                _libraryTreeController.CreateDocumentLocaly(
                    _corutineQueue[_hand],
                    _realQueue[_corutineQueue[_hand]],
                    _states,
                    _hand
                );
                _hand++;
                Debug.Log("я жду создания читалки");
                yield return new WaitUntil(() => _states.Count == _hand);
                Debug.Log("читалка создана, ждем окончания чтения");
                yield return new WaitUntil(() => _states[index].state == CoroutineState.Finished );
                Debug.Log("состояние "+_states[index].state);
                Debug.Log("Начинаем новую короутину");
                QueueCorutine(_hand).StartCoroutine(out _coroutine, out _);
                _coroutineController.Add(_coroutine);
            }
            _coroutineController[index].StopCoroutine();
            yield return null;
        }

        
    }
}
using System.Collections;
using System.Collections.Generic;
using Coroutine;
using iTextSharp.text;
using PDFWorker;
using UnityEngine;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class MainTheoryController
    {
        private readonly TheoryController _theoryController;
        private readonly LibraryTreeController _libraryTreeController;
        private Dictionary<int, int> _corutineQueue;
        private Dictionary<int, string> _realQueue;
        private int _countOfUnitInQueue;
        private int _hand;
        
        public MainTheoryController(
            TheoryController theoryController, 
            LibraryTreeController libraryTreeController
             )
        {
            _theoryController = theoryController;
            _libraryTreeController = libraryTreeController;
            _corutineQueue = new Dictionary<int, int>();
            _realQueue = new Dictionary<int, string>();
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

        //public IEnumerator StartDoingSomeThingWithQueue()
        public void StartDoingSomeThingWithQueue()
        {
            QueueCorutine(_hand).StartCoroutine(out _);
        }

        private IEnumerator QueueCorutine(int index)
        {
            Debug.Log("Creating New document "+index);
            yield return new WaitForEndOfFrame();
            if (index == _countOfUnitInQueue)
            {
                _theoryController.LoadDocumentTheory();
                yield break;
            }

            if (index == 0)
            {
                _theoryController.CreateDocumentLocaly(_corutineQueue[_hand], _realQueue[_corutineQueue[_hand]]);
                _hand++;
                QueueCorutine(_hand).StartCoroutine(out _);
            }
            else
            {
                _libraryTreeController.CreateDocumentLocaly(_corutineQueue[_hand],_realQueue[_corutineQueue[_hand]]);
                _hand++;
                QueueCorutine(_hand).StartCoroutine(out _);
            }
            yield return null;
        }
    }
}
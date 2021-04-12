using System.Collections.Generic;
using Animation;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public sealed class AnimatorController: IInitialization,ICleanData,IExecute
    {
        //int - id of GM, List - actions
        private Dictionary<int, Dictionary<int,IGameHandler>> _actionsList;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly AddingInChainController _addingInChainController;

        public AnimatorController(GameContextWithLogic gameContextWithLogic,AddingInChainController addingInChainController)
        {
            _gameContextWithLogic = gameContextWithLogic;
            _addingInChainController = addingInChainController;
            _actionsList.Add(0,new Dictionary<int,IGameHandler>());
        }

        public void Initialization()
        {
            _addingInChainController.AddNewAction += AddActionToDictionary;
        }

        private void AddActionToDictionary(int id)
        {
            var count = 0;
            foreach (var dictionary in _actionsList)
            {
                foreach (var values in dictionary.Value)
                {
                    if (values.Key == id && count == _actionsList.Count - 1)
                    {
                        _actionsList.Add(_actionsList.Count,new Dictionary<int,IGameHandler>());
                        break;
                    }
                }
                count++;
            }
            
            //_actionsList[id].Add(id,CreateAction());
        }

        private void PutActionInGroup(int from, int where)
        {
            
        }
        
        private IGameHandler CreateAction(TypesOfAnimation type,int id)
        {
            IGameHandler forReturn = null;
            switch (type)
            {
                case TypesOfAnimation.Move:
                    forReturn = new MoveToPosition(_gameContextWithLogic.GameObjectModels[id].gameObjectStruct.GameObject,new Vector3(1,1,1),10);
                    break;
                case TypesOfAnimation.Rotate:
                    forReturn = new RotateAFewSeconds(_gameContextWithLogic.GameObjectModels[id].gameObjectStruct.GameObject,1,1);
                    break;
            }
            return forReturn;
        }

        public void CleanData()
        {
            _addingInChainController.AddNewAction -= AddActionToDictionary;
        }

        public void Execute(float deltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
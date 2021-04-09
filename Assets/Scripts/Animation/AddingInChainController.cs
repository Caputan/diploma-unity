using System;
using Diploma.Controllers;
using Interfaces;
using UnityEngine;

namespace Animation
{
    public sealed class AddingInChainController: IAnimatorAdding
    {

        private readonly Camera _camera;
        private readonly GameContextWithLogic _gameContextWithLogic;
        
        public AddingInChainController(GameContextWithLogic gameContextWithLogic)
        {
            _gameContextWithLogic = gameContextWithLogic;
            _camera = _gameContextWithLogic.MainCamera;
        }

        public void WaitingForMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                    //_gameContextWithLogic.GameObjectModels[0].gameObjectStruct.GameObject
                    AddingAction(hit.transform.gameObject.GetInstanceID());
                }
                else
                {
                    Debug.DrawRay(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
            }
        }
        
        public void AddingAction(int gameObj)
        {
            AddNewAction?.Invoke(gameObj);
        }

        public event Action<int> AddNewAction;
    }
}
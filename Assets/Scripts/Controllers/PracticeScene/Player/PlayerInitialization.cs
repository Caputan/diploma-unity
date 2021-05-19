using Data;
using Diploma.Interfaces;
using UnityEngine;

namespace Diploma.Controllers
{
    public class PlayerInitialization: IExecute
    {
        private readonly GameObject _playerGameObject;
        private readonly PlayerController _playerController;

        private readonly Transform _spawnPoint;
        private GameObject playerGO;
        private bool _isPaused;
            
        public PlayerInitialization(GameObject player, Transform spawnPoint, 
            ImportantDontDestroyData importantDontDestroyData)
        {
            _playerGameObject = player;
            _spawnPoint = spawnPoint;

            _isPaused = false;
            
            playerGO = GameObject.Instantiate(_playerGameObject, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint);
            
            _playerController = new PlayerController(playerGO, importantDontDestroyData);
        }

        public void TurnOnOffCamera(bool toogle,Camera mainCamera)
        {
            playerGO.GetComponentInChildren<Camera>().enabled = toogle;
            mainCamera.enabled = !toogle;
        }
        
        public void SetPause(bool pause)
        {
            _isPaused = pause;
        }
        
        public void Execute(float deltaTime)
        {
            if (_isPaused == false)
            {
                _playerController.RotateCamera(); 
                _playerController.MovePlayer();
                _playerController.OutlineAssemblyParts();
            }
            
        }
    }
}
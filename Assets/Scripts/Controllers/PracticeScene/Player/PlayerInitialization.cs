using Diploma.Interfaces;
using UnityEngine;

namespace Diploma.Controllers
{
    public class PlayerInitialization: IExecute
    {
        private readonly GameObject _playerGameObject;
        private readonly PlayerController _playerController;

        private readonly Transform _spawnPoint;
            
        public PlayerInitialization(GameObject player, Transform spawnPoint)
        {
            _playerGameObject = player;
            _spawnPoint = spawnPoint;
            
            var playerGO = GameObject.Instantiate(_playerGameObject, _spawnPoint, true);
            playerGO.transform.position = _spawnPoint.position;

            _playerController = new PlayerController(playerGO);
        }
        
        public void Execute(float deltaTime)
        {
            _playerController.RotateCamera();
            _playerController.MovePlayer();
            _playerController.OutlineAssemblyParts();
        }
    }
}
using GameObjectCreating;
using UnityEngine;
using UnityEngine.UI;



namespace Diploma.Controllers
{
    public class ControleerInitializer : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int countOfdetails;
        
        private GameContext _gameContext;
        private Controllers _controllers;
        private void Start()
        {
            _gameContext = new GameContext();
            var GameObjectFactory = new GameObjectFactory();
            var Pool = new PoolOfObjects(countOfdetails,GameObjectFactory,_gameContext);
            var GameObjectInitilization = new GameObjectInitialization(Pool);
            
            _controllers = new Controllers();
            
            
            _controllers.Initialization();
        }
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.CleanData();
        }
    }
}
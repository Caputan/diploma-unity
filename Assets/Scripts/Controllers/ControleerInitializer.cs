using Diploma.UI;
using GameObjectCreating;
using UnityEngine;
using UnityEngine.UI;



namespace Diploma.Controllers
{
    public class ControleerInitializer : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int countOfdetails;
        [SerializeField] private Toggle toggle1;
        [SerializeField] private Toggle toggle2;
        [SerializeField] private Toggle toggle3;
        
        private GameContext _gameContext;
        private Controllers _controllers;
        private void Start()
        {
            _gameContext = new GameContext();
            //тут еще нужно разместить создание меню.
            _gameContext.AddToggles(toggle1);
            _gameContext.AddToggles(toggle2);
            _gameContext.AddToggles(toggle3);
            //-----
            
            
            // Scene
            var GameObjectFactory = new GameObjectFactory();
            var Pool = new PoolOfObjects(countOfdetails,GameObjectFactory,_gameContext);
            var GameObjectInitilization = new GameObjectInitialization(Pool);
            
            _controllers = new Controllers();
            _controllers.Add(new ToggleView(_gameContext));
            
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
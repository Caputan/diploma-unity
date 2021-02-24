using Diploma.Constructor;
using Diploma.UI;
using GameObjectCreating;
using UnityEngine;
using UnityEngine.UI;



namespace Diploma.Controllers
{
    public class ControleerInitializer : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int countOfDetails;
        [SerializeField] private GameObject togglePanelPrefab;
        [SerializeField] private Transform CanvasTransform;
        
        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithViews _gameContextWithViews;
        private Controllers _controllers;
        private void Start()
        {
            #region Creation UI and GameContext
            
            _gameContextWithLogic = new GameContextWithLogic();
            _gameContextWithViews = new GameContextWithViews();
            //костыль.
            var GameContextWithViewCreator = new GameContexWithViewCreator(
                _gameContextWithViews,
                _gameContextWithLogic,
                CanvasTransform,
                togglePanelPrefab);
            
            #endregion

            #region Creation new Lession Module

            var abstractFactory = new AbstractFactory();
            var abstractView = new AbstractView(_gameContextWithViews,_gameContextWithLogic);
            var abstractFactoryController = new AbstractFactoryController(abstractView,abstractFactory);
            
            #endregion
           
            
            // Scene
            var GameObjectFactory = new GameObjectFactory();
            var Pool = new PoolOfObjects(countOfDetails,GameObjectFactory,_gameContextWithLogic);
            var GameObjectInitilization = new GameObjectInitialization(Pool);
            
            _controllers = new Controllers();
            _controllers.Add(GameContextWithViewCreator);
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
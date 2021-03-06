using Controllers;
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
        [SerializeField] private GameObject ToggleGroup;
        
        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithViews _gameContextWithViews;
        private Controllers _controllers;
        private void Start()
        {
            #region Creation UI and GameContext
            
            _gameContextWithLogic = new GameContextWithLogic();
            _gameContextWithViews = new GameContextWithViews();
           
            // тут мы создали базове типизированное меню
            var GameContextWithViewCreator = new GameContexWithViewCreator(
                _gameContextWithViews,
                _gameContextWithLogic,
                ToggleGroup,
                togglePanelPrefab);
            
            #endregion

            #region DataBase initialization

            var DatabaseController = new DataBaseController();
            
            #endregion
            
            #region Creation new Lession Module

            var abstractFactory = new AbstractFactory();
            var abstractView = new AbstractView(_gameContextWithViews,_gameContextWithLogic,_button);
            var abstractFactoryController = new AbstractFactoryController(abstractView,abstractFactory);
            
            #endregion
           
            
            // Scene
            var GameObjectFactory = new GameObjectFactory();
            var Pool = new PoolOfObjects(countOfDetails,GameObjectFactory,_gameContextWithLogic);
            var GameObjectInitilization = new GameObjectInitialization(Pool);
            var DataBaseController = new DataBaseController();
            
            _controllers = new Controllers();
            _controllers.Add(GameContextWithViewCreator);
            _controllers.Add(DataBaseController);
            _controllers.Add(abstractView);
            _controllers.Add(abstractFactoryController);
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
using UnityEngine;

namespace Diploma.Controllers
{
    public class ControleerInitializer : MonoBehaviour
    {
        private Controllers _controllers;
        private void Start()
        {
            //коменты для ознакомления с структурой
            // var playerFactory = new PlayerFactory(_data.Player);
            // var playerInitialization = new PlayerInitialization(playerFactory,_gameContext);

            
            _controllers = new Controllers();
            // _controllers.Add(playerInitialization);
            
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
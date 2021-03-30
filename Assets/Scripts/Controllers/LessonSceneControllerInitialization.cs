using UnityEngine;



namespace Diploma.Controllers
{
    public class LessonSceneControllerInitialization: MonoBehaviour
    {

        private Controllers _controllers;
        public string[] destinationPath = new string[4];
        public void Start()
        {
            
            
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
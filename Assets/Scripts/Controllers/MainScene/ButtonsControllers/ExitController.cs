using System.Collections;
using Data;
using Diploma.Interfaces;
using UnityEngine;

namespace Controllers
{
    public class ExitController: IInitialization, IExecute
    {
        private readonly ImportantDontDestroyData _data;

        public ExitController(ImportantDontDestroyData data)
        {
            _data = data;
        }
        public void Initialization() { }

        public void ExitApplication()
        {
            Application.Quit();
        }

        public void Execute(float deltaTime)
        {
            if ((Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
                && Input.GetKeyDown(KeyCode.F4) )
            {
                _data.activatedUserID = -1;
                _data.lessonID = -1;
            }
        }
    }
}
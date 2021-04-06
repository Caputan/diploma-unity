using Diploma.Interfaces;
using UnityEngine;

namespace Controllers
{
    public class ExitController: IInitialization
    {
        public void Initialization() { }

        public void ExitApplication()
        {
            Application.Quit();
        }
    }
}
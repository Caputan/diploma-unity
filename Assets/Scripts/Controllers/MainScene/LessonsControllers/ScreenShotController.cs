using Diploma.Interfaces;
using UnityEngine;

namespace Controllers.MainScene.LessonsControllers
{
    public class ScreenShotController
    {

        public void TakeAScreanShoot(string obj)
        {
            ScreenCapture.CaptureScreenshot(obj);
        }

        
    }
}
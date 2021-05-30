using System.Collections;
using System.Collections.Generic;
using Controllers;
using Data;
using Diploma.Controllers;
using Diploma.Enums;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_StateMachineModulsWorks
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Test_StateMachineWorksExitControllerOK()
        {
            var exit = new ExitController(ScriptableObject.CreateInstance<ImportantDontDestroyData>());
            Application.quitting += () => Assert.Pass();
            exit.ExitApplication();
            Application.quitting -= () => Assert.Pass();
        }

        [Test]
        public void Test_StateMachineWorksBackControllerOK()
        {
            var back = new BackController();
            back.Initialization();
            back.WhereIMustBack(LoadingParts.LoadMain);
            var where = back.GoBack();
            Assert.AreEqual(LoadingParts.LoadMain, where);
        }
    }
}

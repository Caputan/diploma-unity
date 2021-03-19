using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LessonsChooseInitialization : IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _lessonChooseParent;
        private readonly GameObject _prefabLessonChoose;
        private LessonChooseFactory _lessonChooseFactory;
        private List<Button> _lessonChooseButtons;
        
        public LessonsChooseInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,GameObject LessonChooseParent, GameObject PrefabLessonChoose)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _lessonChooseParent = LessonChooseParent;
            _prefabLessonChoose = PrefabLessonChoose;

            _lessonChooseFactory = new LessonChooseFactory(_prefabLessonChoose);
            
        }
        public void Initialization()
        {
            #region Main Menu Creation

            var LessonsChoose = _lessonChooseFactory.Create(_lessonChooseParent.transform);
            LessonsChoose.transform.localPosition = new Vector3(0,0,0);
            
            _lessonChooseButtons = new List<Button>();
            _lessonChooseButtons.AddRange(LessonsChoose.GetComponentsInChildren<Button>());
            
            // new LessonChooseAddButtonsToDictionary(_lessonChooseButtons,_gameContextWithViews);
            //
            // var LessonChooseLogic = new LessonChooseLogic(_gameContextWithViews.Buttons);
            // LessonChooseLogic.Initialization();
            // _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadMain, LessonsChoose);
            // _gameContextWithUI.AddUILogic(LoadingParts.LoadMain,LessonChooseLogic);
            
            #endregion
        }
    }
}
using System;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Constructor
{
    public class GearBoxFactoryView: IFactoryView,IInitialization,IChooseGearboxLowType
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly Button _button;
        private int _toggleID;
        public GearBoxFactoryView(GameContextWithViews gameContextWithViews, GameContextWithLogic gameContextWithLogic
        //временный костыль с кнопкой. кнопки будут в вью
        , Button button
        )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLogic = gameContextWithLogic;
            _button = button;
        }
        
        public void Initialization()
        {
            // тут нужно продумать 2 вьюшки

            #region First View with another toggles
            
            foreach (var toggle in _gameContextWithViews.ChoosenToggles)
            {
                toggle.Value.GetComponentInChildren<Toggle>().onValueChanged.AddListener(on=>
                {
                    if (on) _toggleID = toggle.Value.GetInstanceID();
                });
            }
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => ChoosedNextStage());
            #endregion
        }
        
        public void ChoosedNextStage()
        {
           
        }

        public void LoadNextUi(GameObject content)
        {
            
        }
        
        public void ChoosedLowType(TypesOfGearBoxes typesOfGearBoxes)
        {
            ChooseTypeOf.Invoke(typesOfGearBoxes);
        }

        public event Action<float, int> NextStage;
        public event Action<TypesOfGearBoxes> ChooseTypeOf;
        
    }
}
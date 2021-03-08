using System;
using System.Collections.Generic;
using Controllers;
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
        public List<ITable> Tables { get; }
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly Button _button;
        private readonly FileManagerController _fileManagerController;
        private readonly DataBaseController _dataBaseController;
        private int _toggleID;
        public GearBoxFactoryView(GameContextWithViews gameContextWithViews, GameContextWithLogic gameContextWithLogic
        //сюда еще должен прийти 3дс лоадер
        , Button button,
        FileManagerController fileManagerController
        )
        {
         
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLogic = gameContextWithLogic;
            _button = button;
            _fileManagerController = fileManagerController;
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
                
                toggle.Value.SetActive(false);
            }
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => ChoosedNextStage());
            _button.enabled = false;

            #endregion

           
        }

        public void ChoosedNextStage()
        {
           NextStage.Invoke(LoadingParts.LoadLections);
        }

        public void LoadNextUi(GameObject content)
        {
            _fileManagerController.ShowSaveDialog(FileTypes.Assebly);
            
            //тут нужно загрузить сборку в виде листа
            
            //еще нужно выключить первый вью
            
            #region Second View

            // тут мы объявляем то,что на выходе у лоадера
            // foreach (var VARIABLE in COLLECTION)
            // {
            //     
            
            // setActive(false);
            // }

            #endregion
        }
        
        public void ChoosedLowType(TypesOfGearBoxes typesOfGearBoxes)
        {
            ChooseTypeOf.Invoke(typesOfGearBoxes);
        }

        public event Action<LoadingParts> NextStage;
        public event Action<TypesOfGearBoxes> ChooseTypeOf;
        
    }
}
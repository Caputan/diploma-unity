using System;
using Controllers;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine.UI;

namespace Diploma.Constructor
{
    public class AbstractView: IAbstractView, IInitialization
    {
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly GameContextWithViews _gameContextWithViews;
        private Button _button;
        private readonly FileManagerController _fileManager;
        private int _toggleID;

        public AbstractView(GameContextWithViews gameContextWithViews, GameContextWithLogic gameContextWithLogic,
            Button button,
            FileManagerController fileManager
        )
        {
            _gameContextWithLogic = gameContextWithLogic;
            _button = button;
            _fileManager = fileManager;
            _gameContextWithViews = gameContextWithViews;
        }
        
        public event Action<FactoryType> NextStage;
        public void Initialization()
        {
            foreach (var toggle in _gameContextWithViews.ChoosenToggles)
            {
                toggle.Value.GetComponentInChildren<Toggle>().onValueChanged.AddListener(on=>
                {
                    if (on) _toggleID = toggle.Value.GetInstanceID();
                });
            }
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => ChoosedNextStage());
        }
        
        public void ChoosedNextStage()
        {
            _fileManager.ShowSaveDialog(FileTypes.Assembly);
            NextStage.Invoke(_gameContextWithLogic.FactoryTypeForCreating[_toggleID]);
        }
    }
}
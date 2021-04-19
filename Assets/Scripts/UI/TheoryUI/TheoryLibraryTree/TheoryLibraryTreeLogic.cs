using System;
using Diploma.Enums;
using Interfaces;
using UnityEngine.UI;

namespace UI.TheoryUI.TheoryLibraryTree
{
    public class TheoryLibraryTreeLogic: ILibraryOnSceneButton
    {
        private readonly int _idLibraryItem;
        private readonly Button _button;
        
        public event Action<int> LoadNext;
        
        public TheoryLibraryTreeLogic(int idLibraryItem, Button button)
        {
            _idLibraryItem = idLibraryItem;
            _button = button;
            
        }
        public void Initialization()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(()=> SwitchToNextMenu(_idLibraryItem));
        }

        public void SwitchToNextMenu(int loadingParts)
        {
            LoadNext?.Invoke(loadingParts);
        }


        public void CleanData()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
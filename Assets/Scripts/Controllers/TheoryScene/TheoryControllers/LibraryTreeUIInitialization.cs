using System;
using System.Collections.Generic;
using Data;
using Diploma.Controllers;
using Diploma.Interfaces;
using TMPro;
using UI.TheoryUI;
using UI.TheoryUI.TheoryLibraryTree;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class LibraryTreeUIInitialization: IInitialization
    {
        
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private readonly string _types;
        private readonly AdditionalInfomationLibrary _library;
        private Transform _treeParent;
        private TheoryLibraryTreeFactory _theoryLibraryTreeFactory;
        public LibraryTreeUIInitialization(
            GameObject prefabTreeWindow,
            GameContextWithViewsTheory gameContextWithViewsTheory,
            string types,
            AdditionalInfomationLibrary library
        )
        {
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _types = types;
            _library = library;


            _theoryLibraryTreeFactory = new TheoryLibraryTreeFactory(prefabTreeWindow);
        }
        public void Initialization()
        {
            #region Library Tree UI Creation
            _treeParent = _gameContextWithViewsTheory.Parents[1];
            foreach (var libraryItem in _types.Split(','))
            {
                if (libraryItem != "")
                {
                    var libraryTreeUI = _theoryLibraryTreeFactory.Create(_treeParent);
                    libraryTreeUI.GetComponent<TextMeshProUGUI>().text =
                        _library.libraryObjcets[Convert.ToInt32(libraryItem)].name;
                    libraryTreeUI.GetComponent<Button>().onClick.AddListener(() => OpenLibraryItem(libraryItem));
                }
            }
            
            #endregion
        }

        private void OpenLibraryItem(string libraryItem)
        {
            // сюда передаем data. и воспроизводим новое окно
        }
    }
}
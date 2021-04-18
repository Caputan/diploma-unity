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
        private readonly Transform _treeParent;
        private readonly string _types;
        private readonly AdditionalInfomationLibrary _library;
        private TheoryLibraryTreeFactory _theoryLibraryTreeFactory;
        public LibraryTreeUIInitialization(
            GameObject prefabTreeWindow,
            Transform treeParent,
            string types,
            AdditionalInfomationLibrary library
        )
        {
            _treeParent = treeParent;
            _types = types;
            _library = library;


            _theoryLibraryTreeFactory = new TheoryLibraryTreeFactory(prefabTreeWindow);
        }
        public void Initialization()
        {
            #region Library Tree UI Creation

            var sm = _treeParent.GetChild(4).GetChild(0).GetChild(0).transform;
            
            foreach (var libraryItem in _types.Split(','))
            {
                if (libraryItem != "")
                {
                    Debug.Log(libraryItem);
                    //надо будет добавить на всяки случай проверку на пустую строку или пробел
                    var libraryTreeUI = _theoryLibraryTreeFactory.Create(sm);
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
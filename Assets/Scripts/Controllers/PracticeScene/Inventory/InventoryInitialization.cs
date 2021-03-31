﻿using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.PracticeScene.Inventory
{
    public class InventoryInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _mainParent;
        private readonly GameObject _inventoryPrefab;
        private readonly GameObject _inventorySlotPrefab;
        private readonly InventoryFactory _inventoryFactory;
        private List<Button> InventoryButtons;

        private PlayerController _player;
        private GameObject[] _parts;
        

        public InventoryInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, GameObject MainParent, GameObject inventoryPrefab, 
            GameObject[] parts, GameObject inventorySlotPrefab)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _mainParent = MainParent;
            _inventoryPrefab = inventoryPrefab;
            _inventorySlotPrefab = inventorySlotPrefab;
            _parts = parts;
            _inventoryFactory = new InventoryFactory(_inventoryPrefab);
        }
        
        public void Initialization()
        {
            var inventory = _inventoryFactory.Create(_mainParent.transform);
            
            _player = new PlayerController(GameObject.Find("Player(Clone)"));
            
            inventory.transform.localPosition = Vector3.zero;
            
            InventoryButtons = new List<Button>();

            foreach (var part in _parts)
            {
                var inventorySlot = GameObject.Instantiate(_inventorySlotPrefab, inventory.transform, true);
                inventorySlot.transform.localPosition = Vector3.zero;
                
                // прикрепить изображение к кнопке
                
                InventoryButtons.Add(inventorySlot.GetComponent<Button>());
            }

            new InventoryAddButtonsToDictionary(_parts, InventoryButtons, _gameContextWithViews);
            var inventoryLogic = new InventoryLogic(_gameContextWithViews.InventoryButtons, _player);
            inventoryLogic.Initialization();
        }
    }
}
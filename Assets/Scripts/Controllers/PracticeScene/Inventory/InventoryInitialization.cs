using System.Collections.Generic;
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
        private readonly InventoryFactory _inventoryFactory;
        private List<Button> InventoryButtons;

        private PlayerController _player;
        private GameObject[] _parts;
        

        public InventoryInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, GameObject MainParent, GameObject inventoryPrefab, 
            GameObject[] parts, PlayerController player)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _mainParent = MainParent;
            _inventoryPrefab = inventoryPrefab;
            _parts = parts;
            _player = player;
            _inventoryFactory = new InventoryFactory(_inventoryPrefab);
        }
        
        public void Initialization()
        {
            var inventory = _inventoryFactory.Create(_mainParent.transform);
            
            inventory.transform.localPosition = Vector3.zero;

            InventoryButtons = new List<Button>();
            InventoryButtons.AddRange(inventory.GetComponentsInChildren<Button>());

            new InventoryAddButtonsToDictionary(InventoryButtons, _gameContextWithViews, _parts);
            var inventoryLogic = new InventoryLogic(_gameContextWithViews.InventoryButtons, _player);
            inventoryLogic.Initialization();
        }
    }
}
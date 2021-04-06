using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class InventoryFactory : IUIObjectsFactory
    {
        private GameObject _inventoryPrefab;
        
        public InventoryFactory(GameObject inventoryPrefab)
        {
            _inventoryPrefab = inventoryPrefab;
        }
        
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_inventoryPrefab, Parent, true);
            return gm;
        }
    }
}

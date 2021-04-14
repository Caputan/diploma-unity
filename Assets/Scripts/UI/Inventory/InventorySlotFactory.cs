using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class InventorySlotFactory: IUIObjectsFactory
    {
        private GameObject _inventorySlotPrefab;
        
        public InventorySlotFactory(GameObject inventorySlotPrefab)
        {
            _inventorySlotPrefab = inventorySlotPrefab;
        }
        
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_inventorySlotPrefab, Parent, true);
            return gm;
        }
    }
}
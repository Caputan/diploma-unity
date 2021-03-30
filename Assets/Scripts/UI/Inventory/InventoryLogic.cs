using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class InventoryLogic: IInitialization
    {
        private readonly Dictionary<GameObject, Button> _buttons;
        private readonly PlayerController _player;

        public InventoryLogic(Dictionary<GameObject, Button> buttons, PlayerController player)
        {
            _player = player;
            _buttons = buttons;
        }

        private void GiveToPlayer(GameObject objectToPick)
        {
            _player.PickUp(objectToPick);
        }
        
        public void Initialization()
        {
            foreach (var button in _buttons)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(() => GiveToPlayer(button.Key));
            }
        }
    }
}
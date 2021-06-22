﻿using System;
using System.Collections.Generic;
using Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CompleteUI
{
    public class CompleteLogic: IInitialization
    {
        private readonly Dictionary<CompleteButtons, Button> _buttons;

        private readonly LoadingSceneController _loadingSceneController;
        
        public CompleteLogic(Dictionary<CompleteButtons, Button> buttons, LoadingSceneController loadingSceneController)
        {
            _buttons = buttons;
            _loadingSceneController = loadingSceneController;
        }

        private void SwitchToNextMenu()
        {
            Debug.Log("Button is pressed");
            _loadingSceneController.SetActiveSceneAndLoadIt(0);
        }

        public void Initialization()
        {
            foreach (var button in _buttons)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(SwitchToNextMenu);
            }
        }
    }
}
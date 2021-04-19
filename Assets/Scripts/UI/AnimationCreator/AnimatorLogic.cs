using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace UI.AnimationCreator
{
    public sealed class AnimatorLogic: IAnimatorButtons, IInitialization
    {
        private readonly Dictionary<AnimatorButtons, Button> _buttons;

        public AnimatorLogic(Dictionary<AnimatorButtons, Button> buttons)
        {
            _buttons = buttons;
        }
        public void DoSomeAnimatorAction(AnimatorButtons animatorButtons)
        {
            ButtonPressed?.Invoke(animatorButtons);
        }

        public event Action<AnimatorButtons> ButtonPressed;

        public void Initialization()
        {
            foreach (var button in _buttons)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(() => DoSomeAnimatorAction(button.Key));
            }
        }

        
    }
}
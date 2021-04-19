using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface IAnimatorButtons: IUIObject
    {
        void DoSomeAnimatorAction(AnimatorButtons animatorButtons);
        event Action<AnimatorButtons> ButtonPressed;
    }
}
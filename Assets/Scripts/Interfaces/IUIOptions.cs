using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface IUIOptions: IUIObject
    {
        void SwitchToNextMenu(OptionsButtons loadingParts);
        event Action<OptionsButtons> LoadNext;
    }
}
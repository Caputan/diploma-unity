using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface IUIMainMenu: IUIObject
    {
        void SwitchToNextMenu(LoadingParts loadingParts);
        event Action<LoadingParts> LoadNext;
    }
}
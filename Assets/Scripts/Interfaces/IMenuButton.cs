using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface IMenuButton: IUIObject
    {
        void SwitchToNextMenu(LoadingParts loadingParts);
        event Action<LoadingParts> LoadNext;
    }
}
using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface IPauseButtons: IUIObject
    {
        void SwitchToNextMenu(PauseButtons loadingParts);
        event Action<PauseButtons> LoadNext;
    }
}
using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface ITheorySceneButton: IUIObject
    {
        void SwitchToNextMenu(LoadingPartsTheoryScene loadingParts);
        event Action<LoadingPartsTheoryScene> LoadNext;
    }
}
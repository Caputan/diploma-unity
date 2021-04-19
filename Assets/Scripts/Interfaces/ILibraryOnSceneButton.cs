using System;

namespace Interfaces
{
    public interface ILibraryOnSceneButton: IUIObject
    {
        void SwitchToNextMenu(int loadingParts);
        event Action<int> LoadNext;
    }
}
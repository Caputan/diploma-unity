using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface ICreatingAssemblyButton: IUIObject
    {
        void SwitchToNextMenu(AssemblyCreating loadingParts);
        event Action<AssemblyCreating> LoadNext;
    }
}
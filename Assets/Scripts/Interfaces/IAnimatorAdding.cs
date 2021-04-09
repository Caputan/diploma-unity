using System;

namespace Interfaces
{
    public interface IAnimatorAdding
    {
        void AddingAction(int gameHandler);
        event Action<int> AddNewAction;
    }
}
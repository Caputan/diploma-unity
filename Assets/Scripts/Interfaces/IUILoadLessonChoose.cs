using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface IUILoadLessonChoose: IUIObject
    {
        void SwitchToNextMenu(int id);
        
        event Action<int> LoadNext;
    }
}
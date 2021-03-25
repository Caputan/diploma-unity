using System;


namespace Interfaces
{
    public interface IUISilder: IUIObject
    {
        void SwitchPersent(float persent);
        
        event Action<float> ChangePersent;
    }
}
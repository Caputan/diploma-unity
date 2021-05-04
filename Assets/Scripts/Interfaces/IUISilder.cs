using System;
using Diploma.Enums;


namespace Interfaces
{
    public interface IUISilder: IUIObject
    {
        void SwitchPersent(OptionsButtons optionsButtons,float persent);
        
        event Action<OptionsButtons,float> ChangePersent;
    }
}
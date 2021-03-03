using System;

namespace Interfaces
{
    public interface IFactoryView
    {
        void ChoosedNextStage();
        event Action<float,int> NextStage;
    }
}
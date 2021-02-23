using System;

namespace Interfaces
{
    public interface IFactoryView
    {
        void ChoosedNextStage(bool nextStage);
        event Action<float,int> NextStage;
    }
}
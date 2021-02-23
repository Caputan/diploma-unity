using System;
using Diploma.Enums;

namespace Diploma.Interfaces
{
    public interface IAbstractView
    {
        void ChoosedNextStage();
        event Action<FactoryType> NextStage;
    }
}
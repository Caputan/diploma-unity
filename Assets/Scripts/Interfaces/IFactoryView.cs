using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface IFactoryView
    {
        void ChoosedNextStage();
        event Action<LoadingParts> NextStage;
    }
}
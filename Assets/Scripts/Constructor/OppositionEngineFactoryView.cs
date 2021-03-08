using System;
using Diploma.Enums;
using Interfaces;

namespace Diploma.Constructor
{
    public class OppositionEngineFactoryView: IFactoryView
    {
        public void ChoosedNextStage()
        {
            throw new NotImplementedException();
        }

        public event Action<LoadingParts> NextStage;
    }
}
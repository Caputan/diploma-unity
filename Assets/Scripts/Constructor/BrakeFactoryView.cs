using System;
using Interfaces;

namespace Diploma.Constructor
{
    public class BrakeFactoryView: IFactoryView
    {
        public void ChoosedNextStage()
        {
            throw new NotImplementedException();
        }

        public event Action<float, int> NextStage;
    }
}
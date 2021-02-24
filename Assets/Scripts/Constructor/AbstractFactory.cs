using System;
using Diploma.Enums;
using Diploma.Interfaces;

namespace Diploma.Constructor
{
    public class AbstractFactory: IAbstractFactory
    {
        private FactoryType currentType;
        public void Create(FactoryType factoryType)
        {
            switch (factoryType)
            {
                case FactoryType.Brake:
                    BrakeFactory brakeFactory = new BrakeFactory();
                    brakeFactory.Create();
                    break;
                case FactoryType.GearBox:
                    GearBoxFactory gearBoxFactory = new GearBoxFactory();
                    gearBoxFactory.Create();
                    break;
                case FactoryType.OppositionEngine:
                    OppositionEngineFactory oppositionEngineFactory = new OppositionEngineFactory();
                    oppositionEngineFactory.Create();
                    break;
            }
        }
    }
}
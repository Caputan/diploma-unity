using Diploma.Enums;
using Diploma.Interfaces;

namespace Diploma.Constructor
{
    public class AbstractFactory: IAbstractFactory
    {
        private FactoryType currentType;
        
        
        
        public void Create()
        {
            // switch (enemy.type)
            // {
            //     case "mage":
            //         MageFactory mage = new MageFactory(enemy.type,enemy.health);
            //         CreateFactory(mage);
            //         break;
            //     case "infantry":
            //         IfantryFactory ifantry = new IfantryFactory(enemy.type,enemy.health); 
            //         CreateFactory(ifantry);
            //         break;
            //          
            // }
        }
    }
}
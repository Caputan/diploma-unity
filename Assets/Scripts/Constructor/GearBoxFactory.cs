using System;
using Diploma.Enums;
using Interfaces;
using UnityEngine;

namespace Diploma.Constructor
{
    public class GearBoxFactory: IConstructorFactory
    {
        public void Create()
        {
            //так. не будет оно использоваться. надо подумать как объединить нижний метод
            Debug.Log("GearBoxFactory was created");
        }

        public GameObject ChooseAType(TypesOfGearBoxes typesOfGearBoxes)
        {
            switch (typesOfGearBoxes)
            {
                case TypesOfGearBoxes.Conical:
                    return new GameObject();
                    
                case TypesOfGearBoxes.Cylindrical:
                    return new GameObject();
                    
                case TypesOfGearBoxes.Worm_like:
                    return new GameObject();
                
                default:
                    throw new Exception("Type Error");
            }
        }
    }
}
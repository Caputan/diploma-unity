using System;
using System.Collections.Generic;
using Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;

namespace Diploma.Constructor
{
    public class GearBoxFactory: IConstructorFactory
    {
        private readonly DataBaseController _dataBaseController;
        private readonly List<ITable> _tables;

        public GearBoxFactory(DataBaseController dataBaseController, List<ITable> tables)
        {
            _dataBaseController = dataBaseController;
            _tables = tables;
        }
        
        public void Create()
        {
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
                    
                case TypesOfGearBoxes.WormLike:
                    return new GameObject();
                
                default:
                    throw new Exception("Type Error");
            }
        }
        
    }
}
using Interfaces;
using UnityEngine;

namespace Diploma.Constructor
{
    public class GearBoxFactory: IConstructorFactory
    {
        public void Create()
        {
            Debug.Log("GearBoxFactory was created");
        }
    }
}
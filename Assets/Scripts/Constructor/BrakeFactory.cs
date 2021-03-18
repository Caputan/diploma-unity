using Diploma.Interfaces;
using UnityEngine;

namespace Diploma.Constructor
{
    public class BrakeFactory: IConstructorFactory
    {
        public void Create()
        {
            Debug.Log("BrakeFactory was created");
        }
    }
}
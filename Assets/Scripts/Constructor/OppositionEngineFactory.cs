using Interfaces;
using UnityEngine;

namespace Diploma.Constructor
{
    public class OppositionEngineFactory: IConstructorFactory
    {
        public void Create()
        {
            Debug.Log("OppositionEngineFactory was created");
        }
    }
}
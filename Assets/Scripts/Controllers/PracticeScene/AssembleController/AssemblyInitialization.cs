using Diploma.Interfaces;
using UnityEngine;

namespace Diploma.Controllers.AssembleController
{
    public class AssemblyInitialization: IInitialization
    {
        private AssembleController _assembleController;
        
        private readonly GameObject _basePart;
        private readonly GameObject[] _partsOfAssembly;
        
        public AssemblyInitialization(GameObject basePart, GameObject[] partsOfAssembly)
        {
            _basePart = basePart;
            _partsOfAssembly = partsOfAssembly;
            
            _assembleController = new AssembleController(_basePart, _partsOfAssembly);
        }
        
        public void Initialization()
        {
            
        }
    }
}
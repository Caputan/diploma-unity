using Diploma.Interfaces;
using UnityEngine;

namespace Diploma.Controllers.AssembleController
{
    public class AssemblyInitialization: IInitialization
    {
        private AssembleController _assembleController;
        
        private readonly GameObject _assemblyGameObject;

        private readonly Transform _assemblyParent;
        
        public AssemblyInitialization(GameObject assemblyGameObject, string order, Transform assemblyParent)
        {
            _assemblyGameObject = assemblyGameObject;
            _assemblyParent = assemblyParent;
            
            _assembleController = new AssembleController(order);
        }
        
        public void Initialization()
        {
            var gm = GameObject.Instantiate(_assemblyGameObject, _assemblyParent);
            gm.transform.localScale = new Vector3(5f, 5f, 5f);

            var meshes = gm.GetComponentsInChildren<MeshRenderer>();
            int partId = 0;
            foreach (var mesh in meshes)
            {
                var outline = mesh.gameObject.AddComponent<Outline>();
                outline.part_Id = partId;
                
                Debug.Log(mesh.name + " | " + partId);
                
                partId++;
                
                var rb = mesh.gameObject.AddComponent<Rigidbody>();
                rb.isKinematic = true; 
                
                mesh.gameObject.AddComponent<MeshCollider>();
                
                mesh.tag = "Assembly";
            }
        }
    }
}
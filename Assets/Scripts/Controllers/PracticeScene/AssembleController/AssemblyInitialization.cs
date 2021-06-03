using System.Collections.Generic;
using Controllers;
using Data;
using Diploma.Extensions;
using Diploma.Interfaces;
using UnityEngine;

namespace Diploma.Controllers.AssembleController
{
    public class AssemblyInitialization: IInitialization
    {
        private AssembleController _assembleController;
        
        private readonly GameObject _assemblyGameObject;

        private readonly Transform _assemblyParent;
        private GameObject gm;
        
        public AssemblyInitialization(GameObject assemblyGameObject, string order, Transform assemblyParent)
        {
            _assemblyGameObject = assemblyGameObject;
            _assemblyParent = assemblyParent;
            
            _assembleController = new AssembleController(order);
        }

        public AssemblyInitialization(GameObject assemblyGameObject, Transform assemblyParent)
        {
            _assemblyGameObject = assemblyGameObject;
            _assemblyParent = assemblyParent;
        }
        
        public void Initialization()
        {
            gm = GameObject.Instantiate(_assemblyGameObject, _assemblyParent);
            gm.transform.localScale = new Vector3(5f, 5f, 5f);
            // gm.transform.position = Vector3.zero;

            // var animator = gm.GetComponent<Animator>();
            // animator.enabled = false;
            
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

        public GameObject GetAGameObject()
        {
            return gm;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Diploma.Interfaces;
using UnityEngine;

namespace Diploma.Controllers.AssembleController
{
    public sealed class AssemblyCreator
    {
        private string _order;
        private Outline[] _outlines;
        public event Action<string> EndCreatingEvent;
        
        public AssemblyCreator(
        )
        {
            PlayerController.OnPartClicked += AddToOrder;
        }

        public void SetAssemblyGameObject(GameObject assemblyGameObject)
        {
            _outlines = assemblyGameObject.GetComponentsInChildren<Outline>();
        }
        
        private void AddToOrder(GameObject partOfAssembly)
        {
            var partID = partOfAssembly.GetComponent<Outline>().part_Id;
            _order += partID + " ";
            
            if (partOfAssembly.transform.parent.childCount > 1)
            {
                for (var i = 0; i < partOfAssembly.transform.parent.childCount; i++)
                {
                    partOfAssembly.transform.parent.GetComponentsInChildren<MeshRenderer>()[i].enabled = false;
                }
                partOfAssembly.GetComponent<MeshCollider>().enabled = false;
            }
            else
            {
                partOfAssembly.GetComponent<MeshCollider>().enabled = false;
                partOfAssembly.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        public void BackInOrder()
        {
            var whatIsBack = _order.Split(' ').Last();
            foreach (var outline in _outlines)
            {
                if (outline.part_Id == Convert.ToInt32(whatIsBack))
                {
                    if (outline.gameObject.transform.parent.childCount > 1)
                    {
                        for (var i = 0; i < outline.gameObject.transform.parent.childCount; i++)
                        {
                            outline.gameObject.transform.parent.GetComponentsInChildren<MeshRenderer>()[i].enabled = true;
                        }
                        outline.gameObject.GetComponent<MeshCollider>().enabled = true;
                    }
                    else
                    {
                        outline.gameObject.GetComponent<MeshCollider>().enabled = true;
                        outline.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
            Debug.Log(_order);
            _order.Remove(_order.Length - (whatIsBack.Length+1));
            Debug.Log(_order);
        }

        public void EndCreating()
        {
            EndCreatingEvent?.Invoke(_order);
        }
    }
}
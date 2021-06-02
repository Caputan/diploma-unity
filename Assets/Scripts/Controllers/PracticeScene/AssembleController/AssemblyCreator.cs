using System;
using System.Collections.Generic;
using System.Linq;
using Diploma.Interfaces;
using TMPro;
using Tools;
using UI.CreatingAssemblyUI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Diploma.Controllers.AssembleController
{
    public sealed class AssemblyCreator: ICleanData
    {
        private string _order;
        private Outline[] _outlines;
        private GameObject _whereShouldPutTheInfo;
        private CreatingAssemblyFactory _creatingAssemblyFactory;
        public event Action<string> EndCreatingEvent;
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/OrderUnit"};
        
        public AssemblyCreator()
        {
            PlayerController.OnPartClicked += AddToOrder;
            _creatingAssemblyFactory = new CreatingAssemblyFactory(ResourceLoader.LoadPrefab(_viewPath));
            
        }

        public void SetAssemblyGameObject(GameObject assemblyGameObject,GameObject whereShouldPutTheInfo)
        {
            _outlines = assemblyGameObject.GetComponentsInChildren<Outline>();
            _whereShouldPutTheInfo = whereShouldPutTheInfo;
            var animator = assemblyGameObject.GetComponent<Animator>();
            animator.enabled = false;
            _order = null;
        }
        
        private void AddToOrder(GameObject partOfAssembly)
        {
            var partID = partOfAssembly.GetComponent<Outline>().part_Id;
            _order += partID + " ";
            Debug.Log("Пополнение "+_order);
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

            var components = _creatingAssemblyFactory.Create(_whereShouldPutTheInfo.transform).GetComponentsInChildren<TextMeshProUGUI>();
            components[0].text = partID.ToString();
            components[1].text = partOfAssembly.transform.parent.name;

        }

        public void BackInOrder()
        {
            var whatIsBack = _order.Split(' ');
            foreach (var outline in _outlines)
            {
                if (outline.part_Id == Convert.ToInt32(whatIsBack[whatIsBack.Length-2]))
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
            _order = _order.Remove(_order.Length-2);

            var componentToDelete =
                _whereShouldPutTheInfo.transform.GetChild(_whereShouldPutTheInfo.transform.childCount - 1);
            Object.Destroy(componentToDelete.gameObject);
        }

        public void EndCreating()
        {
            EndCreatingEvent?.Invoke(_order);
        }

        public void CleanData()
        {
            PlayerController.OnPartClicked -= AddToOrder;
        }
    }
}
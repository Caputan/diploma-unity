using System;
using System.Collections;
using Coroutine;
using UnityEngine;

namespace Diploma.Controllers.AssembleController
{
    public class AssembleController
    {
        private readonly string _order;
        private GameObject[] _partsOfAssembly;

        private int _index;

        private bool _isDisassembling;

        public AssembleController(string order)
        {
            _index = 0;
            _order = order;

            _partsOfAssembly = new GameObject[_order.Length];

            _isDisassembling = true;
            
            PlayerController.OnPartClicked += StartDisassembling;
        }

        private void StartDisassembling(GameObject partOfAssembly)
        {
            var partID = partOfAssembly.GetComponent<Outline>().part_Id;
            var disassembleOrder = _order.Split(' ');
            if (partID != int.Parse(disassembleOrder[_index])) return;
            if (_isDisassembling)
            {
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

                _partsOfAssembly[_index] = partOfAssembly;
                _index++;
            }
            else
            {
                partOfAssembly.GetComponent<MeshRenderer>().enabled = true;
                _index--;
                _partsOfAssembly[_index + 1].GetComponent<MeshCollider>().enabled = false;
                if(_index > -1)
                    _partsOfAssembly[_index].GetComponent<MeshCollider>().enabled = true;
            }
            
            if (_index == _order.Length)
            {
                _isDisassembling = false;
                _index--;
                _partsOfAssembly[_index].GetComponent<MeshCollider>().enabled = true;
            }
        }
    }
}
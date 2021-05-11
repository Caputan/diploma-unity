using System;
using System.Collections;
using Coroutine;
using UnityEngine;

namespace Diploma.Controllers.AssembleController
{
    public class AssembleController
    {
        private GameObject _basePart;
        // private readonly string[] _disassembleOrder = 
        // {
        //     "Ось прижимного рычага",
        //     "Ось прижимного рычага",
        //     "Рычаг суппорта",
        //     "Рычаг суппорта",
        //     "Болт M10x1.25x25",
        //     "Болт M10x1.25x25",
        //     "Коллектор",
        //     "Штуцер",
        //     "Штуцер",
        //     "Пружина",
        //     "Пружина",
        //     "Пружина",
        //     "Пружина",
        //     "Защитный кожух суппорта",
        //     "Блок цилиндров",
        //     "Поршень",
        //     "Поршень",
        //     "Поршень",
        //     "Направляющая колодок",
        //     "Кожух защитный"
        // };
        private readonly GameObject[] _partsOfAssembly;

        private int _index;

        private bool _isDisassembling;

        public AssembleController(GameObject basePart, GameObject[] partsOfAssembly)
        {
            _index = 0;
            _basePart = basePart;
            _partsOfAssembly = partsOfAssembly;

            _isDisassembling = true;

            int partId = 0;
            foreach (var part in _partsOfAssembly)
            {
                var outline = part.AddComponent<Outline>();
                outline.part_Id = partId;
                partId++;
                
                var rb = part.AddComponent<Rigidbody>();
                rb.isKinematic = true; 
                
                part.AddComponent<MeshCollider>();
                
                part.tag = "Assembly";
            }

            Debug.Log(_partsOfAssembly[_index].transform.parent.name.Split(':')[0]);
            
            PlayerController.OnPartClicked += StartDisassembling;
        }

        private void StartDisassembling(GameObject partOfAssembly)
        {
            if (partOfAssembly.transform.parent.name.Split(':')[0] 
                != _partsOfAssembly[_index].transform.parent.name.Split(':')[0]) return;
            if (_isDisassembling)
            {
                if (_partsOfAssembly[_index].transform.parent.childCount > 1)
                {
                    for (var i = 0; i < _partsOfAssembly[_index].transform.parent.childCount; i++)
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
            
            if (_index == _partsOfAssembly.Length)
            {
                _isDisassembling = false;
                _index--;
                _partsOfAssembly[_index].GetComponent<MeshCollider>().enabled = true;
            }
        }
    }
}
using System.Collections;
using Coroutine;
using UnityEngine;

namespace Diploma.Controllers.AssembleController
{
    public class AssembleController
    {
        private GameObject _basePart;
        private string[] _disassembleOrder = 
        {
            "Ось прижимного рычага",
            "Ось прижимного рычага",
            "Рычаг суппорта",
            "Рычаг суппорта",
            "Болт M10x1.25x25",
            "Болт M10x1.25x25",
            "Коллектор",
            "Штуцер",
            "Штуцер",
            "Пружина",
            "Пружина",
            "Пружина",
            "Пружина",
            "Защитный кожух суппорта",
            "Блок цилиндров",
            "Поршень",
            "Поршень",
            "Поршень",
            "Направляющая колодок",
            "Кожух защитный"
        };
        private GameObject[] _partsOfAssembly;
        private GameObject _partToDisassemble;

        private int _index;

        public AssembleController(GameObject basePart, GameObject[] partsOfAssembly)
        {
            _index = 0;
            _basePart = basePart;
            _partsOfAssembly = partsOfAssembly;

            PlayerController.OnPartClicked += StartDisassembling;
        }

        private void StartDisassembling(GameObject partOfAssembly)
        {
            if (partOfAssembly.transform.parent.name == _disassembleOrder[_index])
            {
                _index++;
                _partToDisassemble = partOfAssembly;
                DisassemblePart();
            }
            else
            {
                // вызвать сообщение об ошибке
            }
        }

        private void DisassemblePart()
        {
            _partToDisassemble.SetActive(false);
            
        }
    }
}
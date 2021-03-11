using System;
using System.Collections.Generic;
using Controllers;
using Diploma.Interfaces;
using Diploma.Tables;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace ListOfLessons
{
    public sealed class ListOfLessonsView: IInitialization, IShowInfoFromChoosing
    {
        private int _id;
        private readonly GameObject _lessonToggle;
       


        public ListOfLessonsView(int id,GameObject lessonToggle)
        {
            _id = id;
            _lessonToggle = lessonToggle;
            
        }
        public void Initialization()
        {
            _lessonToggle.GetComponent<Toggle>().
                onValueChanged.AddListener((someBool) => ChooseLessionFromList(someBool));
        }
        
        public void ChooseLessionFromList(bool someBool)
        {
            if(someBool)
                ChooseAnotherLession.Invoke(_id);
        }
        public event Action<int> ChooseAnotherLession;
    }
}
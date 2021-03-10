﻿using System;
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
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;


        public ListOfLessonsView(int id,GameObject lessonToggle,
            DataBaseController dataBaseController,List<IDataBase> tables)
        {
            _id = id;
            _lessonToggle = lessonToggle;
            _dataBaseController = dataBaseController;
            _tables = tables;
        }
        public void Initialization()
        {
            _lessonToggle.GetComponent<Toggle>().
                onValueChanged.AddListener((someBool) => ChooseLessionFromList(someBool));
        }
        
        public void ChooseLessionFromList(bool someBool)
        {
            if(someBool)
                ChooseAnotherLession.Invoke(_id,_dataBaseController,_tables);
        }
        public event Action<int,DataBaseController,List<IDataBase>> ChooseAnotherLession;
    }
}
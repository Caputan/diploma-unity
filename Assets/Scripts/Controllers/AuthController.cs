using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;
using TMPro;
using UnityEngine;

namespace Diploma.Controllers
{
    public class AuthController : IInitialization
    {
        private readonly DataBaseController _dataBase;
        public TMP_InputField Login;
        public  TMP_InputField Password;

        public AuthController(DataBaseController dataBase, List<IDataBase> tables)
        {
            _dataBase = dataBase;
            _dataBase.SetTable(tables[4]);
        }

        public bool CheckAuthData()
        {
            var loginedUser = (Users) _dataBase.GetRecordFromTableByName(Login.text);
            if (loginedUser == null || Password.text == "")
            {
                // вывод сообщения о неправильно введенном имени пользователя или пароле
                return false;
            }

            if (Password.text == loginedUser.User_Password)
            {
                return true;
            }

            return false;
        }

        public void AddNewUser(string[] newUserParams)
        {
            if (_dataBase.GetRecordFromTableByName(newUserParams[0]) != null)
                return;
            
            _dataBase.AddNewRecordToTable(newUserParams);
        }


        public void Initialization() { }
    }
}
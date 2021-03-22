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
        public TMP_InputField NewLogin;
        public  TMP_InputField NewPassword;
        public  TMP_InputField NewEmail;

        private readonly IDataBase _table;

        public AuthController(DataBaseController dataBase, List<IDataBase> tables)
        {
            _dataBase = dataBase;
            _table = tables[4];
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

        public bool AddNewUser()
        {
            if (NewLogin.text == "" || NewPassword.text == "" || NewEmail.text == "")
                return false;

            string[] newUserParams = {NewLogin.text, NewEmail.text, NewPassword.text};
            _dataBase.SetTable(_table);
            _dataBase.AddNewRecordToTable(newUserParams);
            return true;
        }


        public void Initialization() { }
    }
}
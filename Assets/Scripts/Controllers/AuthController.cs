using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;
using TMPro;
using UnityEditor;
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
        public TextMeshProUGUI greetings;

        private ErrorCodes _error;

        private readonly IDataBase _table;

        public AuthController(DataBaseController dataBase, List<IDataBase> tables)
        {
            _dataBase = dataBase;
            _table = tables[4];
        }

        public ErrorCodes CheckAuthData()
        {
            _dataBase.SetTable(_table);

            if (Login.text == string.Empty)
            {
                Debug.Log("Working!");
                _error = ErrorCodes.EmptyInputError;
            }
            else
            {
                var loginedUser = (Users) _dataBase.GetRecordFromTableByName(Login.text);
                if (Login.text != "" && loginedUser == null)
                {
                    _error = ErrorCodes.AuthError;
                }
                else if (Password.text != loginedUser.User_Password)
                {
                    _error = ErrorCodes.AuthError;
                }
                else
                {
                    greetings.text = "Привет, " + Login.text;
                    _error = ErrorCodes.None;
                }
            }

            return _error;
        }

        public ErrorCodes AddNewUser()
        {
            if (NewLogin.text == "" || NewPassword.text == "" || NewEmail.text == "")
                return ErrorCodes.EmptyInputError;

            string[] newUserParams = {NewLogin.text, NewEmail.text, NewPassword.text};
            _dataBase.SetTable(_table);
            _dataBase.AddNewRecordToTable(newUserParams);
            return ErrorCodes.None;
        }


        public void Initialization() { }
    }
}
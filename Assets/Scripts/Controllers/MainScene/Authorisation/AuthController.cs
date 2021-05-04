
using System;
using System.Collections.Generic;
using Crypto;
using Data;
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
        private readonly ImportantDontDestroyData _data;
        private readonly CryptoGrath _cryptoGrath;
        private readonly IDataBase _table;
        
        private ErrorCodes _error;
        private Users _loginedUser;
        
        public TMP_InputField Login;
        public  TMP_InputField Password; 
        public TMP_InputField NewLogin;
        public  TMP_InputField NewPassword;
        public  TMP_InputField NewEmail;
        public TMP_Dropdown Role;
        public TextMeshProUGUI Greetings;
        

        public AuthController(
            DataBaseController dataBase,
            List<IDataBase> tables,
            ImportantDontDestroyData data)
        {
            _dataBase = dataBase;
            _data = data;
            _table = tables[4];
            _cryptoGrath = new CryptoGrath();
        }

        public void ReopenUser(out int role)
        {
            _dataBase.SetTable(_table);
            _loginedUser = (Users) _dataBase.GetRecordFromTableById(_data.activatedUserID);
            Greetings.text = "Привет, " + _loginedUser.User_Name;
            role = Convert.ToInt32(_loginedUser.User_Role);
        }
        
        public ErrorCodes CheckAuthData(out int role)
        {
            _dataBase.SetTable(_table);

            if (Login.text == string.Empty)
            {
                Debug.Log("Working!");
                _error = ErrorCodes.EmptyInputError;
            }
            else
            {
                _loginedUser = (Users) _dataBase.GetRecordFromTableByName(Login.text);
                
                if (Login.text != "" && _loginedUser == null)
                {
                    _error = ErrorCodes.AuthError;
                }
                else if (!_cryptoGrath.VerifyPasswordHash(Password.text,
                        _cryptoGrath.ConvertFromStringToByte(_loginedUser.User_Password),
                        _cryptoGrath.ConvertFromStringToByte(_loginedUser.User_Salt)))
                //else if (Password.text != loginedUser.User_Password)
                {
                    _error = ErrorCodes.AuthError;
                }
                else
                {
                    Greetings.text = "Привет, " + Login.text;
                    
                    _error = ErrorCodes.None;
                }
            }
            role = Convert.ToInt32(_loginedUser.User_Role);
            _data.activatedUserID = _loginedUser.User_Id;
            return _error;
        }

        public ErrorCodes AddNewUser()
        {
            if (NewLogin.text == "" || NewPassword.text == "" || NewEmail.text == "" || Role.value == 0)
                return ErrorCodes.EmptyInputError;
            _dataBase.SetTable(_table);
            
            foreach (var currentUser in _dataBase.GetDataFromTable<Users>())
            {
                if (currentUser.User_Email == NewEmail.text)
                    return ErrorCodes.SignUpError;
            }

            _cryptoGrath.CreatePasswordHash(
                NewPassword.text,
                out var hashPass,
                out var hashSalt);
            string[] newUserParams = {
                NewLogin.text, 
                NewEmail.text, 
                _cryptoGrath.ConvertFromByteIntoString(hashPass),
                _cryptoGrath.ConvertFromByteIntoString(hashSalt),
                Convert.ToString(Role.value) 
            };
            
            _dataBase.AddNewRecordToTable(newUserParams);
            return ErrorCodes.None;
        }


        public void Initialization() { }
    }
}
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;

namespace Controllers
{
    public class AuthController : IInitialization
    {
        private readonly DataBaseController _dataBase;

        public AuthController(DataBaseController dataBase, List<IDataBase> tables)
        {
            _dataBase = dataBase;
            _dataBase.SetTable(tables[4]);
        }

        public LoadingParts CheckAuthData(string username, string password)
        {
            Users loginedUser = (Users) _dataBase.GetRecordFromTableByName(username);

            if (loginedUser == null || password == "")
            {
                // вывод сообщения о неправильно введенном имени пользователя или пароле
                return LoadingParts.LoadError;
            }

            if (password == loginedUser.User_Password)
            {
                switch (loginedUser.User_Role)
                {
                    case "Teacher":
                        return LoadingParts.LoadConstructor;

                    case "Student":
                        return LoadingParts.LoadMain;
                }
            }

            return LoadingParts.None;
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
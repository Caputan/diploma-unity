using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_Auth
    {
        [Test]
        public void Test_SingUpAddNewUserEmptyInput()
        {
            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies); // 0 - assembles
            tables.Add(lessons); // 1 - lessons
            tables.Add(texts); // 2 - text
            tables.Add(types); // 3 - types
            tables.Add(users); // 4 - users
            tables.Add(videos); // 5 - videos
            AuthController authController = new AuthController(DataBaseController,tables,
                ScriptableObject.CreateInstance<ImportantDontDestroyData>());
            authController.Initialization();
            SignUpInitialization signUpInitialization = new SignUpInitialization(
                new GameContextWithViews(),new GameContextWithUI(),new GameObject(),authController);
            signUpInitialization.Initialization();

            authController.NewLogin.text = "";
            authController.NewEmail.text = "";
            authController.NewPassword.text = "";
            authController.Role.value = 0;
            var error = authController.AddNewUser();
            Assert.AreEqual(ErrorCodes.EmptyInputError,error);
        }
        [Test]
        public void Test_SingUpAddNewUserValidationLoginError()
        {
            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies); // 0 - assembles
            tables.Add(lessons); // 1 - lessons
            tables.Add(texts); // 2 - text
            tables.Add(types); // 3 - types
            tables.Add(users); // 4 - users
            tables.Add(videos); // 5 - videos
            AuthController authController = new AuthController(DataBaseController,tables,
                ScriptableObject.CreateInstance<ImportantDontDestroyData>());
            authController.Initialization();
            SignUpInitialization signUpInitialization = new SignUpInitialization(
                new GameContextWithViews(),new GameContextWithUI(),new GameObject(),authController);
            signUpInitialization.Initialization();

            authController.NewLogin.text = "123";
            authController.NewEmail.text = "1232121@yandex.ru";
            authController.NewPassword.text = "123132123123";
            authController.Role.value = 1;
            var error = authController.AddNewUser();
            Assert.AreEqual(ErrorCodes.ValidationLoginError,error);
        }
        [Test]
        public void Test_SingUpAddNewUserValidationPasswordError()
        {
            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies); // 0 - assembles
            tables.Add(lessons); // 1 - lessons
            tables.Add(texts); // 2 - text
            tables.Add(types); // 3 - types
            tables.Add(users); // 4 - users
            tables.Add(videos); // 5 - videos
            AuthController authController = new AuthController(DataBaseController,tables,
                ScriptableObject.CreateInstance<ImportantDontDestroyData>());
            authController.Initialization();
            SignUpInitialization signUpInitialization = new SignUpInitialization(
                new GameContextWithViews(),new GameContextWithUI(),new GameObject(),authController);
            signUpInitialization.Initialization();

            authController.NewLogin.text = "dsfsfdsdfsdf";
            authController.NewEmail.text = "111@fssf.ru";
            authController.NewPassword.text = "123";
            authController.Role.value = 1;
            var error = authController.AddNewUser();
            Assert.AreEqual(ErrorCodes.ValidationPasswordError,error);
        }
        [Test]
        public void Test_SingUpAddNewUserValidationEmailError()
        {
            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies); // 0 - assembles
            tables.Add(lessons); // 1 - lessons
            tables.Add(texts); // 2 - text
            tables.Add(types); // 3 - types
            tables.Add(users); // 4 - users
            tables.Add(videos); // 5 - videos
            AuthController authController = new AuthController(DataBaseController,tables,
                ScriptableObject.CreateInstance<ImportantDontDestroyData>());
            authController.Initialization();
            SignUpInitialization signUpInitialization = new SignUpInitialization(
                new GameContextWithViews(),new GameContextWithUI(),new GameObject(),authController);
            signUpInitialization.Initialization();

            authController.NewLogin.text = "WDasdssa";
            authController.NewEmail.text = "wddwdw";
            authController.NewPassword.text = "George10052000";
            authController.Role.value = 1;
            var error = authController.AddNewUser();
            Assert.AreEqual(ErrorCodes.ValidationEmailError,error);
        }
        [Test]
        public void Test_SingUpAddNewUserOKInfo()
        {
            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies); // 0 - assembles
            tables.Add(lessons); // 1 - lessons
            tables.Add(texts); // 2 - text
            tables.Add(types); // 3 - types
            tables.Add(users); // 4 - users
            tables.Add(videos); // 5 - videos
            AuthController authController = new AuthController(DataBaseController,tables,
                ScriptableObject.CreateInstance<ImportantDontDestroyData>());
            authController.Initialization();
            SignUpInitialization signUpInitialization = new SignUpInitialization(
                new GameContextWithViews(),new GameContextWithUI(),new GameObject(),authController);
            signUpInitialization.Initialization();

            authController.NewLogin.text = "PetrovF";
            authController.NewEmail.text = "AAAAFFD@yandex.ru";
            authController.NewPassword.text = "George10052000";
            authController.Role.value = 1;
            var error = authController.AddNewUser();
            Assert.AreEqual(ErrorCodes.None,error);
            DataBaseController.SetTable(tables[4]);
            DataBaseController.DeleteLastRecord(DataBaseController.GetDataFromTable<Users>().Last().User_Id);
        }
        [Test]
        public void Test_AuthCheckAuthDataOK()
        {
            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies); // 0 - assembles
            tables.Add(lessons); // 1 - lessons
            tables.Add(texts); // 2 - text
            tables.Add(types); // 3 - types
            tables.Add(users); // 4 - users
            tables.Add(videos); // 5 - videos
            AuthController authController = new AuthController(DataBaseController,tables,
                ScriptableObject.CreateInstance<ImportantDontDestroyData>());
            authController.Initialization();
            AuthInitialization authInitialization = new AuthInitialization(
                new GameContextWithViews(),new GameContextWithUI(),new GameObject(),authController);
            authInitialization.Initialization();

            authController.Login.text = "Преподаватель";
            authController.Password.text = "123";
            authController.Greetings = new TextMeshProUGUI();
            int role;
            var error = authController.CheckAuthData(out role);
            Assert.AreEqual(ErrorCodes.None,error);
        }
        [Test]
        public void Test_AuthCheckAuthDataNotOK()
        {
            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies); // 0 - assembles
            tables.Add(lessons); // 1 - lessons
            tables.Add(texts); // 2 - text
            tables.Add(types); // 3 - types
            tables.Add(users); // 4 - users
            tables.Add(videos); // 5 - videos
            AuthController authController = new AuthController(DataBaseController,tables,
                ScriptableObject.CreateInstance<ImportantDontDestroyData>());
            authController.Initialization();
            AuthInitialization authInitialization = new AuthInitialization(
                new GameContextWithViews(),new GameContextWithUI(),new GameObject(),authController);
            authInitialization.Initialization();

            authController.Login.text = "Преподаватель";
            authController.Password.text = "456";
            int role;
            var error = authController.CheckAuthData(out role);
            Assert.AreEqual(ErrorCodes.AuthError,error);
        }
        [Test]
        public void Test_AuthCheckAuthDataEmptyInput()
        {
            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies); // 0 - assembles
            tables.Add(lessons); // 1 - lessons
            tables.Add(texts); // 2 - text
            tables.Add(types); // 3 - types
            tables.Add(users); // 4 - users
            tables.Add(videos); // 5 - videos
            AuthController authController = new AuthController(DataBaseController,tables,
                ScriptableObject.CreateInstance<ImportantDontDestroyData>());
            authController.Initialization();
            AuthInitialization authInitialization = new AuthInitialization(
                new GameContextWithViews(),new GameContextWithUI(),new GameObject(),authController);
            authInitialization.Initialization();

            authController.Login.text = "";
            authController.Password.text = "";
            int role;
            var error = authController.CheckAuthData(out role);
            Assert.AreEqual(ErrorCodes.EmptyInputError,error);
        }
    }
}

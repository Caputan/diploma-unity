using Diploma.Enums;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class ErrorHandler
    {
        private GameObject _errorPrefab;
        private TextMeshProUGUI _errorTextUI;
        
        public ErrorHandler(GameObject errorPrefab)
        {
            _errorPrefab = errorPrefab;
            _errorTextUI = _errorPrefab.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ChangeErrorMessage(ErrorCodes error)
        {
            _errorTextUI.text = "";
            switch (error)
            {
                case ErrorCodes.AuthError:
                    _errorTextUI.text = "Неправильно введено имя пользователя или пароль";
                    break;
                case ErrorCodes.SignUpError:
                    _errorTextUI.text = "Такой пользователь уже существует";
                    break;
                case ErrorCodes.EmptyInputError:
                    _errorTextUI.text = "Заполните все поля";
                    break;
                case ErrorCodes.WrongFormatError:
                    _errorTextUI.text = "Формат одного из файлов не соответствует  требуемому";
                    break;
                default:
                    _errorTextUI.text = "Неизвестная ошибка";
                    break;
            }
        }
    }
}
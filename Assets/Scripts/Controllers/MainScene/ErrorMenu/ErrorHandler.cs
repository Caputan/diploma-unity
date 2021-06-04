using Diploma.Enums;
using DoTween;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class ErrorHandler
    {
        private GameObject _errorPrefab;
        private TextMeshProUGUI _errorTextUI;
        public MessageBoxBehaviour MessageBoxBehaviour;
        
        public ErrorHandler(GameObject errorPrefab)
        {
            _errorPrefab = errorPrefab;
            _errorTextUI = _errorPrefab.GetComponentInChildren<TextMeshProUGUI>();
            MessageBoxBehaviour = _errorPrefab.GetComponent<MessageBoxBehaviour>();
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
                case ErrorCodes.ValidationLoginError:
                    _errorTextUI.text = "Имя пользователя должно быть больше 6 и меньше 20 символов";
                    break;
                case ErrorCodes.ValidationPasswordError:
                    _errorTextUI.text = "Пароль должен быть больше 8 символов и содержать как минимум одну цифру, одну строчную и заглавную буквы";
                    break;
                case ErrorCodes.ValidationEmailError:
                    _errorTextUI.text = "Неправильно введенная электронная почта";
                    break;
                case ErrorCodes.FileDoesNotExist:
                    _errorTextUI.text = "Файл не найден по заданному пути или путь пустой";
                    break;
                default:
                    _errorTextUI.text = "Неизвестная ошибка";
                    break;
            }
        }
    }
}
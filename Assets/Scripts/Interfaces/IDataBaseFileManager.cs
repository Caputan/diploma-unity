using System;
using Diploma.Enums;

namespace Interfaces
{
    public interface IDataBaseFileManager
    {
        event Action<LoadingParts,string> newText;

        void ShowNewText(LoadingParts loadingParts,string text);
    }
}
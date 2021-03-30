using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;

namespace Controllers
{
    public class BackController: IInitialization
    {
        private List<LoadingParts> _loadingParts;

        public void Initialization()
        {
            _loadingParts = new List<LoadingParts>();
        }

        public void WhereIMustBack(LoadingParts lastback)
        {
            _loadingParts.Add(lastback);
        }

        public LoadingParts GoBack()
        {
            var back = _loadingParts[_loadingParts.Count - 1];
            _loadingParts.RemoveAt(_loadingParts.Count - 1);
            return back;
        }
    }
}
using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;

namespace Controllers
{
    public class BackController: IInitialization
    {
        private List<LoadingParts> _loadingPartses;

        public void Initialization()
        {
            _loadingPartses = new List<LoadingParts>();
        }

        public void WhereIMustBack(LoadingParts lasstback)
        {
            _loadingPartses.Add(lasstback);
        }

        public LoadingParts GoBack()
        {
            var back = _loadingPartses[_loadingPartses.Count - 1];
            _loadingPartses.RemoveAt(_loadingPartses.Count - 1);
            return back;
        }
    }
}
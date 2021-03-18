using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class TogglePanelFactory: IUIObjectsFactory
    {
        private readonly GameObject _togglePanel;

        public TogglePanelFactory(GameObject togglePanel)
        {
            _togglePanel = togglePanel;
            
        }
        public GameObject Create(Transform parent)
        {
           var gm = GameObject.Instantiate(_togglePanel, parent, true);
           return gm;
        }
    }
}
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;

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
           var gm = GameObject.Instantiate(_togglePanel);
           gm.transform.SetParent(parent);
           return gm;
        }
    }
}
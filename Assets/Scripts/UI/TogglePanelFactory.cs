using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class TogglePanelFactory: IUIObjectsFactory
    {
        private readonly GameObject _togglePanel;
        private readonly ToggleGroup _toggleGroup;

        public TogglePanelFactory(GameObject togglePanel,ToggleGroup toggleGroup)
        {
            _togglePanel = togglePanel;
            _toggleGroup = toggleGroup;
        }
        public GameObject Create(Transform parent)
        {
           var gm = GameObject.Instantiate(_togglePanel, parent, true);

           gm.GetComponentInChildren<Toggle>().group = _toggleGroup;
           return gm;
        }
    }
}
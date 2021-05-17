using Interfaces;
using UnityEngine;

namespace UI.CreatingAssemblyUI
{
    public sealed class CreatingAssemblyFactory: IUIObjectsFactory
    {
        private GameObject _Prefab;

        public CreatingAssemblyFactory(GameObject Prafab)
        {
            _Prefab = Prafab;
        }
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_Prefab, Parent, true);
            return gm;
        }
    }
}
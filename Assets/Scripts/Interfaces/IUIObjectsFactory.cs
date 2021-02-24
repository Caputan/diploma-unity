using UnityEngine;

namespace Interfaces
{
    public interface IUIObjectsFactory
    {
        GameObject Create(Transform Parent);
    }
}
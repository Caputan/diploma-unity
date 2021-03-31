using UnityEngine;

namespace Diploma.Interfaces
{
    public interface IFactory
    {
        GameObject CreateGameObject(GameObject gameObject);
    }
}
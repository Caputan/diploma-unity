using UnityEngine;

namespace GameObjectCreating
{
    public interface IFactory
    {
        GameObject CreateGameObject(int name);
    }
}
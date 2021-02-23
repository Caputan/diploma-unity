using Diploma.Extensions;
using Diploma.Interfaces;
using UnityEngine;

namespace GameObjectCreating
{
    public class GameObjectFactory : IFactory
    {
        public GameObject CreateGameObject( int name)
        {
            return new GameObject($"{name}").
                    AddRigidBody(2).
                    AddCollider()
                ; 
        }
    }
}
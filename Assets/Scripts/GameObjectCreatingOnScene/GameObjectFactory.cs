using Diploma.Extensions;
using Diploma.Interfaces;
using UnityEngine;

namespace GameObjectCreating
{
    public class GameObjectFactory : IFactory
    {
        private readonly bool _rigidGravity;

        public GameObjectFactory(bool rigidGravity)
        {
            _rigidGravity = rigidGravity;
        }
        public GameObject CreateGameObject(GameObject gameObject)
        {
            return gameObject.
                    AddRigidBody(0,_rigidGravity).
                    AddMeshCollider()
                ; 
        }
    }
}
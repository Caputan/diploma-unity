using Diploma.Extensions;
using Diploma.Interfaces;
using UnityEngine;

namespace GameObjectCreating
{
    public class GameObjectFactory : IFactory
    {
        private readonly bool _rigidGravity;
        private readonly Material _material;

        public GameObjectFactory(bool rigidGravity,Material material)
        {
            _rigidGravity = rigidGravity;
            _material = material;
        }
        public GameObject CreateGameObject(GameObject gameObject)
        {
            return gameObject.
                AddRigidBody(0, _rigidGravity).
                AddMeshCollider().
                SetNewMaterial(_material);
            
        }
    }
}
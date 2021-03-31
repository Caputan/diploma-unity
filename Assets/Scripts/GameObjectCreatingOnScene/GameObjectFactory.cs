﻿using Diploma.Extensions;
using Diploma.Interfaces;
using UnityEngine;

namespace GameObjectCreating
{
    public class GameObjectFactory : IFactory
    {
        public GameObject CreateGameObject(GameObject gameObject)
        {
            return gameObject.
                    AddRigidBody(0).
                    AddMeshCollider()
                ; 
        }
    }
}
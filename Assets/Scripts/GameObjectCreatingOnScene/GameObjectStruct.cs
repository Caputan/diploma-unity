using System;
using Interfaces;
using UnityEngine;

namespace GameObjectCreating
{
    public class GameObjectStruct : ICloneable, ICollision
    {
        public GameObject GameObject;

        public object Clone()
        {
           return new GameObjectStruct()
           {
               GameObject = this.GameObject
           };
        }

        public void CollisionDetected()
        {
            LookAtCollision.Invoke(GameObject.GetInstanceID());
        }

        public event Action<int> LookAtCollision;
    }
}
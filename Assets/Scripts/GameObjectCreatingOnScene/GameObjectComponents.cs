using System;
using UnityEngine;

namespace GameObjectCreating
{
    public class GameObjectComponents: ICloneable
    {
        public Rigidbody Rigidbody;
        public Collider Collider;
       
        public object Clone()
        {
            return new GameObjectComponents()
            {
                Rigidbody = this.Rigidbody,
                Collider = this.Collider
            };
            
        }
    }
}
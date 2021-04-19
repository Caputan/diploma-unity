using System;
using UnityEngine;

namespace GameObjectCreating
{
    public class GameObjectComponents: ICloneable
    {
        public Rigidbody Rigidbody;
        public Collider Collider;
        public MeshRenderer MeshRenderer;
        public MeshFilter MeshFilter;
       
        public object Clone()
        {
            return new GameObjectComponents()
            {
                Rigidbody = this.Rigidbody,
                Collider = this.Collider,
                MeshRenderer = this.MeshRenderer,
                MeshFilter = this.MeshFilter
            };
            
        }
    }
}
using System;
using UnityEngine;

namespace Diploma.Extensions
{
    public static partial class Extensions
    {
         public static GameObject SetName(this GameObject gameObject, string name)
        {
            gameObject.name = name;
            return gameObject;
        }


        public static GameObject AddRigidBody(this GameObject gameObject,float mass,bool gravity)
        { 
            var component = gameObject.GetOrAddComponent<Rigidbody>();
            component.mass = mass;
            component.useGravity = gravity;
            return gameObject;
        }

        public static GameObject AddMeshCollider(this GameObject gameObject)
        {
            var component = gameObject.GetOrAddComponent<MeshCollider>();
            return gameObject;
        }
        public static GameObject AddBoxCollider(this GameObject gameObject)
        {
            var component = gameObject.GetOrAddComponent<BoxCollider>();
            return gameObject;
        }
        
        public static GameObject AddSpriteRenderer(this GameObject gameObject, Sprite sprite)
        {
            var component = gameObject.GetOrAddComponent<SpriteRenderer>();
            component.sprite = sprite;
            return gameObject;
        }

        public static GameObject SetNewMaterial(this GameObject gameObject, Material material)
        {
            var component = gameObject.GetOrAddComponent<MeshRenderer>();
            component.material = material;
            return gameObject;
        }
        // public static GameObject AddMeshFilter(this GameObject gameObject, Mesh mesh)
        // {
        //     var component = gameObject.GetOrAddComponent<MeshFilter>();
        //     component.mesh = mesh;
        //     return gameObject;
        // }

        public static GameObject AddTag(this GameObject gameObject, string tag)
        {
            gameObject.tag = tag;
            return gameObject;
        }
        

        private static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var result = gameObject.GetComponent<T>();
            if (!result)
            {
                result = gameObject.AddComponent<T>();
            }

            return result;
        }

        public static bool TryBool(this string self)
        {
            return Boolean.TryParse(self, out var res) && res;
        }

        public static float TrySingle(this string self)
        {
            if (Single.TryParse(self, out var res))
            {
                return res;
            }
            return 0;
        }
    }
}
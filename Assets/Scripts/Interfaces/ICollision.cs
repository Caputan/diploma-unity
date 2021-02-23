using System;

namespace Interfaces
{
    public interface ICollision
    {
        void CollisionDetected();
        event Action<int> LookAtCollision;
    }
}
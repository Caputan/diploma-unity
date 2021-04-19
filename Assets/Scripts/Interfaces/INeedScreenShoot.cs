using System;

namespace Interfaces
{
    public interface INeedScreenShoot
    {
        event Action<string> TakeScreenShoot;
        void TakingScreen(string name);
    }
}
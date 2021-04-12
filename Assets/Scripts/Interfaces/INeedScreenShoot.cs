using System;

namespace Interfaces
{
    public interface INeedScreenShoot
    {
        event Action<string> TakeScreanShoot;
        void TakingScreen(string name);
    }
}
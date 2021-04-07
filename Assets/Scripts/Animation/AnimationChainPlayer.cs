using System.Collections.Generic;

namespace Animation
{
    public class AnimationChainPlayer
    {
        public void StartAnimation(List<GameHandler> gameHandlers)
        {
            gameHandlers[0].Handle();
            for (var i = 1; i < gameHandlers.Count; i++) 
            {
                gameHandlers[i - 1].SetNext(gameHandlers[i]);
            }
        }
    }
}
using Interfaces;

namespace Animation
{
    public abstract class GameHandler:IGameHandler
    {
        private IGameHandler _nextHandler;

        public virtual IGameHandler Handle()
        {
            if (_nextHandler != null)
            {
                _nextHandler.Handle();
            }
            return _nextHandler;
        }

        public IGameHandler SetNext(IGameHandler nextHandler)
        {
            _nextHandler = nextHandler;
            return nextHandler;
        }
    }
}
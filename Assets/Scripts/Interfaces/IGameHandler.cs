namespace Interfaces
{
    public interface IGameHandler
    {
        IGameHandler Handle();
        IGameHandler SetNext(IGameHandler nextHandler);
    }
}
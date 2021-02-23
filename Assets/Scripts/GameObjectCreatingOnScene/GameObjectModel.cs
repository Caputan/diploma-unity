namespace GameObjectCreating
{
    public class GameObjectModel
    {
        private readonly GameObjectComponents _gameObjectComponents;
        private readonly GameObjectStruct _gameObjectStruct;

        public GameObjectModel(GameObjectComponents gameObjectComponents, GameObjectStruct gameObjectStruct)
        {
            _gameObjectComponents = gameObjectComponents;
            _gameObjectStruct = gameObjectStruct;
        }
    }
}
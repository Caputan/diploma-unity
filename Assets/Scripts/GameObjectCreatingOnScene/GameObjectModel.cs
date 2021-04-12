namespace GameObjectCreating
{
    public class GameObjectModel
    {
        public readonly GameObjectComponents gameObjectComponents;
        public readonly GameObjectStruct gameObjectStruct;

        public GameObjectModel(GameObjectComponents gameObjectComponents, GameObjectStruct gameObjectStruct)
        {
            this.gameObjectComponents = gameObjectComponents;
            this.gameObjectStruct = gameObjectStruct;
        }
    }
}
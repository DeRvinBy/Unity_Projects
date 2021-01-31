using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using SharpDX;

namespace GameLibrary.Maze
{
    /// <summary>
    /// Класс фабрики создания элементов лабиринта 
    /// </summary>
    public class MazeElementsFactory
    {
        /// <summary>
        /// Создает элемент лабиринта
        /// </summary>
        /// <param name="position">Позиция объекта на сцене</param>
        /// <param name="TagName">Тег игрового объекта</param>
        /// <returns>Созданный игровой объект</returns>
        public GameObject CreateMazeElement(Vector2 position, string TagName)
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(position, new Size2F(1f, 1f)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/MazeElements/" + TagName + ".png")));

            if (TagName == "Platform")
                gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(1f, 0.1f), new Vector2(0, -0.5f)));
            else
                gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(1f, 1f)));

            gameObject.GameObjectTag = TagName;

            if (TagName == "BreakWall")
                gameObject.InitializeObjectScript(new BreakWall());

            return gameObject;
        }

        /// <summary>
        /// Создание монет в лабиринте
        /// </summary>
        /// <param name="position">Позиция объекта на сцене</param>
        /// <returns>Игровой объект</returns>
        public GameObject CreateCoin(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(position, new Size2F(1f, 1f)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/MazeElements/Treasure.png")));
            gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(1f, 1f)));

            gameObject.GameObjectTag = "Coin";

            gameObject.InitializeObjectScript(new Coin());

            return gameObject;
        }
    }
}

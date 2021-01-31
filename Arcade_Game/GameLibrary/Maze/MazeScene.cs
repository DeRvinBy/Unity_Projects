using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using SharpDX;
using System;
using System.Drawing;
using System.Collections.Generic;
using GameLibrary.Game;

namespace GameLibrary.Maze
{
    /// <summary>
    /// Класс лабиринта
    /// </summary>
    public class MazeScene : Scene
    {
        /// <summary>
        /// Статическая ссылка на класс
        /// </summary>
        public static MazeScene instance = null;
        /// <summary>
        /// Фабрика создания элементов лабиринта
        /// </summary>
        public MazeElementsFactory ElementsFactory { get; private set; }
        /// <summary>
        /// Конструктор синего игрока
        /// </summary>
        public PlayerConstructor BluePlayerFactory { get; set; }
        /// <summary>
        /// Конструктор красного игрока
        /// </summary>
        public PlayerConstructor RedPlayerFactory { get; set; }

        private List<Vector2> emptyBlocks = new List<Vector2>();
        private int countOfcoins = 0;

        /// <summary>
        /// Создание игровых объектов на сцене
        /// </summary>

        protected override void CreateGameObjectsOnScene()
        {
            if (instance == null)
                instance = this;

            ElementsFactory = new MazeElementsFactory();
            BluePlayerFactory = new PlayerConstructor();
            RedPlayerFactory = new PlayerConstructor();

            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(new Vector2(0f, 0f), new Size2F(27, 15)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/Фон.png")));
            gameObject.GameObjectTag = "Background";

            gameObjects.Add(gameObject);

            gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(new Vector2(0f, 0f), new Size2F(1, 1)));
            gameObject.GameObjectTag = "GameManager";
            gameObject.InitializeObjectScript(new SpawnManager());

            gameObjects.Add(gameObject);

            CreateMaze();

            GameObject player = BluePlayerFactory.CreatePlayer("Blue Player");

            gameObjects.Add(player);
            gameObjects.Add(BluePlayerFactory.CreateArms());

            player = RedPlayerFactory.CreatePlayer("Red Player");

            gameObjects.Add(player);
            gameObjects.Add(RedPlayerFactory.CreateArms());
        }

        /// <summary>
        /// Метод создания лабиринта
        /// </summary>
        public void CreateMaze()
        {
            Random random = new Random();

            string text = "Resources/Mazes/Maze " + random.Next(1, 6).ToString() + ".bmp";

            Bitmap bitmap = new Bitmap(text);

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    System.Drawing.Color color = bitmap.GetPixel(j, i);

                    GameObject gameObject = null;

                    if (color.R == 0 && color.G == 0 && color.B == 0)
                        gameObject = ElementsFactory.CreateMazeElement(new Vector2(j, i), "Wall");
                    else if (color.R == 255 && color.G == 0 && color.B == 0)
                        gameObject = ElementsFactory.CreateMazeElement(new Vector2(j, i), "BreakWall");
                    else if (color.R == 0 && color.G == 255 && color.B == 0)
                        gameObject = ElementsFactory.CreateMazeElement(new Vector2(j, i), "Platform");
                    else if (color.R == 0 && color.G == 0 && color.B == 255)
                        gameObject = ElementsFactory.CreateMazeElement(new Vector2(j, i), "Stair");
                    else if (color.R == 255 && color.G == 255 && color.B == 0)
                    {
                        gameObject = ElementsFactory.CreateCoin(new Vector2(j, i));
                        countOfcoins++;
                    }
                    else if (color.R == 125 && color.G == 0 && color.B == 0)
                        RedPlayerFactory.StartPosition = new Vector2(j, i);
                    else if (color.R == 0 && color.G == 0 && color.B == 125)
                        BluePlayerFactory.StartPosition = new Vector2(j, i);
                    else
                        emptyBlocks.Add(new Vector2(j, i));

                    if (gameObject != null)
                        gameObjects.Add(gameObject);
                }
            }
        }

        /// <summary>
        /// Добавление объекта в лист отрисовки
        /// </summary>
        /// <param name="gameObject">Игровой объект</param>
        public void AddObjectOnScene(GameObject gameObject)
        {
            gameObjectsToAdd.Add(gameObject);
        }

        /// <summary>
        /// Удаления объекта из листа отрисовки
        /// </summary>
        /// <param name="gameObject">Игровой объект</param>
        public void RemoveObjectFromScene(GameObject gameObject)
        {
            if (gameObject.GameObjectTag == "Spawn")
                emptyBlocks.Add(gameObject.Transform.Position);

            gameObjectsToRemove.Add(gameObject);

            if (gameObject.GameObjectTag == "Coin")
            {
                countOfcoins--;
                if(countOfcoins == 0)
                    EndScene();
            }
        }

        /// <summary>
        /// Рандомное место в лабиринте
        /// </summary>
        /// <returns>Позицию</returns>
        public Vector2 GetRandomPosition()
        {
            Random random = new Random();

            int index = random.Next(0, emptyBlocks.Count);

            Vector2 position = emptyBlocks[index];

            emptyBlocks.Remove(position);

            return position;
        }

        /// <summary>
        /// Поведение при завершении сцены
        /// </summary>
        protected override void EndScene()
        {
            base.EndScene();

            string winPlayer = "";
            int bluePlayerCountCoins = (BluePlayerFactory.PlayerGameObject.Script as Player).Coins;
            int redPlayerCountCoins = (RedPlayerFactory.PlayerGameObject.Script as Player).Coins;

            if(bluePlayerCountCoins < redPlayerCountCoins)
                winPlayer = RedPlayerFactory.PlayerTag;
            else
                winPlayer = BluePlayerFactory.PlayerTag;

            GameEvents.EndGame?.Invoke(winPlayer);
        }
    }
}

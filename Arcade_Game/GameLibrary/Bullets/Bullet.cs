using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Maze;
using SharpDX;

namespace GameLibrary.Bullets
{
    /// <summary>
    /// Абстрактный класс пули
    /// </summary>
    public abstract class Bullet : ObjectScript
    {
        /// <summary>
        /// Скорость пули
        /// </summary>
        public abstract float Speed { get; }

        /// <summary>
        /// Экземпляр сцены игры
        /// </summary>
        protected MazeScene maze;

        private Vector2 flyDirection;
        private string interactionTag;

        /// <summary>
        /// Установление направления полета пули
        /// </summary>
        /// <param name="direction">Вектор направления</param>
        /// <param name="tag">Тег игрового объекта, создающий пулю</param>
        public void SetSettings(Vector2 direction, string tag)
        {
            flyDirection = direction;

            if (tag == "Blue Player")
                interactionTag = "Red Player";
            else
                interactionTag = "Blue Player";
        }

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        public override void Start()
        {
            maze = MazeScene.instance;
        }

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        public override void Update()
        {
            Vector2 movement = flyDirection * Speed * Time.DeltaTime;
            gameObject.Transform.SetMovement(movement);

            if (gameObject.Collider.CheckIntersection("Wall"))
                maze.RemoveObjectFromScene(gameObject);

            if (gameObject.Collider.CheckIntersection(out BreakWall wall))
            {
                maze.RemoveObjectFromScene(gameObject);
                wall.DestroyWall();
            }

            if (gameObject.Collider.CheckIntersection(out GameObject playerGameObject, interactionTag))
            {
                PlayerInteraction(playerGameObject);
                maze.RemoveObjectFromScene(gameObject);
            }
        }

        /// <summary>
        /// Взаимодействие с игроком
        /// </summary>
        public abstract void PlayerInteraction(GameObject playerGameObject);
    }
}

using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Game;
using GameLibrary.Maze;
using SharpDX;

namespace GameLibrary.Guns
{
    /// <summary>
    /// Абстрактный класс оружия
    /// </summary>
    public abstract class Gun : ObjectScript
    {
        /// <summary>
        /// Время использования оружия
        /// </summary>
        public abstract float UseTime { get; }

        /// <summary>
        /// Время перезарядки оружия
        /// </summary>
        public abstract float ReloadTime { get; }

        /// <summary>
        /// Фабрика персонажа, к которому прикреплено оружие
        /// </summary>
        public PlayerConstructor PlayerFactory { get; set; }

        /// <summary>
        /// Экземпляр сцены игры
        /// </summary>
        protected MazeScene maze;

        private Player playerScript;
        private float currentReloadTime;
        private float currentUseTime;

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        public override void Start()
        {
            playerScript = ((Player)gameObject.ParentGameObject.Script);
            maze = MazeScene.instance;

            currentReloadTime = Time.CurrentTime + ReloadTime;
            currentUseTime = Time.CurrentTime + UseTime;

            LoadAnimation();
        }

        /// <summary>
        /// Загрузка анимации оружия
        /// </summary>
        protected abstract void LoadAnimation();

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        public override void Update()
        {
            if (playerScript.IsCanMove && Input.GetButtonDawn(playerScript.Control.ShootKey) && currentReloadTime < Time.CurrentTime)
            {
                Vector2 bulletSpawnPosition = gameObject.ParentGameObject.Transform.Position;
                Vector2 bulletDirection = new Vector2(gameObject.Sprite.IsFlipX ? -1 : 1, 0);
                SpawnBullet(bulletSpawnPosition, bulletDirection);

                currentReloadTime = Time.CurrentTime + ReloadTime;
            }

            if (currentUseTime < Time.CurrentTime)
            {
                maze.AddObjectOnScene(PlayerFactory.CreateArms());
                maze.RemoveObjectFromScene(gameObject);
            }
        }

        /// <summary>
        /// Создание пули из фабрики
        /// </summary>
        /// <param name="position">Позиция создания</param>
        /// <param name="direction">Направление пули</param>
        protected abstract void SpawnBullet(Vector2 position, Vector2 direction);
    }
}

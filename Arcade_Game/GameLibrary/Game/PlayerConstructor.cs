using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Guns;
using SharpDX;

namespace GameLibrary.Game
{
    /// <summary>
    /// Класс фабрики создания персонажа
    /// </summary>
    public class PlayerConstructor
    {
        /// <summary>
        /// Игровой Объект игрока
        /// </summary>
        public GameObject PlayerGameObject { get; private set; }
        /// <summary>
        /// Тег плеера
        /// </summary>
        public string PlayerTag { get; private set; }
        /// <summary>
        /// Начальная позиция игрока в лабиринте
        /// </summary>
        public Vector2 StartPosition { get; set; }

        /// <summary>
        /// Создание игрового объекта персонажа
        /// </summary>
        /// <returns>Игровой объект</returns>
        public GameObject CreatePlayer(string path)
        {
            PlayerTag = path;

            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(StartPosition, new Size2F(1f, 1f)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/" + PlayerTag + "/left idle 1.png")));
            gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(0.5f, 0.5f), new Vector2(0, 0.2f)));
            gameObject.GameObjectTag = PlayerTag;

            Player playerScript = new Player();
            gameObject.InitializeObjectScript(playerScript);

            PlayerGameObject = gameObject;

            return gameObject;
        }

        /// <summary>
        /// Создание игрового объектиа оружия
        /// </summary>
        /// <returns>Игровой объект</returns>
        public GameObject CreateGun(Gun gun, string nameOfgun)
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(new Vector2(0f, 0f), new Size2F(1, 1)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/" + PlayerTag + "/" + nameOfgun  + " Gun/" + nameOfgun + " left idle 1.png")));
            gameObject.GameObjectTag = "Gun";

            gameObject.ParentGameObject = PlayerGameObject;

            gun.PlayerFactory = this;
            gameObject.InitializeObjectScript(gun);

            Player playerScript = PlayerGameObject.Script as Player;
            playerScript.SetChildGameObject(gameObject);

            GameEvents.ChangeGun?.Invoke(PlayerTag, nameOfgun);

            return gameObject;
        }

        /// <summary>
        /// Создание игрового объекта рук
        /// </summary>
        /// <returns>Игровой объект</returns>
        public GameObject CreateArms()
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(new Vector2(0f, 0f), new Size2F(1, 1)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/" + PlayerTag + "/Arms/arms left idle 1.png")));
            gameObject.GameObjectTag = "Arms";

            Animation animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerTag + "/Arms/arms left idle ", 2), 0.2f, true);
            gameObject.Sprite.AddAnimation("idleLeft", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerTag + "/Arms/arms left run ", 4), 0.2f, true);
            gameObject.Sprite.AddAnimation("runLeft", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerTag + "/Arms/arms right idle ", 2), 0.2f, true);
            gameObject.Sprite.AddAnimation("idleRight", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerTag + "/Arms/arms right run ", 4), 0.2f, true);
            gameObject.Sprite.AddAnimation("runRight", animation);

            gameObject.Sprite.SetAnimation("idleLeft");

            Player playerScript = PlayerGameObject.Script as Player;
            playerScript.SetChildGameObject(gameObject);

            gameObject.ParentGameObject = PlayerGameObject;

            GameEvents.ChangeGun?.Invoke(PlayerTag, "");

            return gameObject;
        }
    }
}

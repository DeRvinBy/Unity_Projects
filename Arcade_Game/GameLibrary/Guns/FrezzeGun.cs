using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Bullets.BulletFactories;
using SharpDX;

namespace GameLibrary.Guns
{
    /// <summary>
    /// Класс оружия заморозки
    /// </summary>
    public class FrezzeGun : GunDecorator
    {
        /// <summary>
        /// Время использования оружия
        /// </summary>
        public override float UseTime => decoratedGun.UseTime + 5f;
        /// <summary>
        /// Время перезарядки оружия
        /// </summary>
        public override float ReloadTime => decoratedGun.ReloadTime - 0.5f;

        /// <summary>
        /// Загрузка анимации оружия
        /// </summary>
        protected override void LoadAnimation()
        {
            Animation animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerFactory.PlayerTag + "/Frezze Gun/frezze left idle ", 2), 0.2f, true);
            gameObject.Sprite.AddAnimation("idleLeft", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerFactory.PlayerTag + "/Frezze Gun/frezze left run ", 4), 0.2f, true);
            gameObject.Sprite.AddAnimation("runLeft", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerFactory.PlayerTag + "/Frezze Gun/frezze right idle ", 2), 0.2f, true);
            gameObject.Sprite.AddAnimation("idleRight", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerFactory.PlayerTag + "/Frezze Gun/frezze right run ", 4), 0.2f, true);
            gameObject.Sprite.AddAnimation("runRight", animation);

            gameObject.Sprite.SetAnimation("idleLeft");
        }

        /// <summary>
        /// Создание пули из фабрики
        /// </summary>
        /// <param name="position">Позиция создания пули</param>
        /// <param name="direction">Направление пули</param>
        protected override void SpawnBullet(Vector2 position, Vector2 direction)
        {
            FrezzeBulletFactory factory = new FrezzeBulletFactory();
            maze.AddObjectOnScene(factory.CreateBullet(position, direction, gameObject.ParentGameObject.GameObjectTag));
        }
    }
}

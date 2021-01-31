using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Bullets.BulletFactories;
using SharpDX;

namespace GameLibrary.Guns
{
    /// <summary>
    /// Класс оружия урона
    /// </summary>
    public class DamageGun : Gun
    {
        /// <summary>
        /// Время использования оружия
        /// </summary>
        public override float UseTime => 10f;

        /// <summary>
        /// Время перезарядки оружия
        /// </summary>
        public override float ReloadTime => 2f;

        /// <summary>
        /// Загрузка анимации оружия
        /// </summary>
        protected override void LoadAnimation()
        {
            Animation animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerFactory.PlayerTag + "/Damage Gun/damage left idle ", 2), 0.2f, true);
            gameObject.Sprite.AddAnimation("idleLeft", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerFactory.PlayerTag + "/Damage Gun/damage left run ", 4), 0.2f, true);
            gameObject.Sprite.AddAnimation("runLeft", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerFactory.PlayerTag + "/Damage Gun/damage right idle ", 2), 0.2f, true);
            gameObject.Sprite.AddAnimation("idleRight", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/" + PlayerFactory.PlayerTag + "/Damage Gun/damage right run ", 4), 0.2f, true);
            gameObject.Sprite.AddAnimation("runRight", animation);

            gameObject.Sprite.SetAnimation("idleLeft");
        }

        /// <summary>
        /// Создание пули из фабрики
        /// </summary>
        /// <param name="position">Позиция создания</param>
        /// <param name="direction">Направление пули</param>
        protected override void SpawnBullet(Vector2 position, Vector2 direction)
        {
            DamageBulletFactory factory = new DamageBulletFactory();
            maze.AddObjectOnScene(factory.CreateBullet(position, direction, gameObject.ParentGameObject.GameObjectTag));
        }
    }
}

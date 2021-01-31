using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using SharpDX;

namespace GameLibrary.Bullets.BulletFactories
{
    /// <summary>
    /// Класс фабрики создания замедляющей пули 
    /// </summary>
    public class SlowdownBulletFactory : BulletFactory
    {
        /// <summary>
        /// Создание игрового объекта пули, которая замедляет
        /// </summary>
        /// <param name="position">Позиция появления пули</param>
        /// <param name="direction">Направление пули</param>
        /// /// <param name="tag">Тег игрового объекта, создающий пулю</param>
        /// <returns>Игровой объект</returns>
        public override GameObject CreateBullet(Vector2 position, Vector2 direction, string tag)
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(position, new Size2F(1, 1)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/Bullets/slowdown bullet.png")));
            gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(0.4f, 0.4f)));
            gameObject.GameObjectTag = "Bullet";

            DamageBullet bullet = new DamageBullet();
            SlowdownBullet slowdownBullet = new SlowdownBullet();
            slowdownBullet.SetDecoratedBullet(bullet);
            slowdownBullet.SetSettings(direction, tag);
            gameObject.InitializeObjectScript(slowdownBullet);

            return gameObject;
        }
    }
}

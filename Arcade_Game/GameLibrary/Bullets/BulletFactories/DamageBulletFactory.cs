using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using SharpDX;

namespace GameLibrary.Bullets.BulletFactories
{
    /// <summary>
    /// Класс фабрики создания пули урона 
    /// </summary>
    public class DamageBulletFactory : BulletFactory
    {
        /// <summary>
        /// Создание игрового объекта пули, которая убивает
        /// </summary>
        /// <param name="position">Позиция появления пули</param>
        /// <param name="direction">Направление пули</param>
        /// <param name="tag">Тег игрового объекта, создающий пулю</param>
        /// <returns>Игровой объект</returns>
        public override GameObject CreateBullet(Vector2 position, Vector2 direction, string tag)
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(position, new Size2F(1, 1)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/Bullets/damage bullet.png")));
            gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(0.4f, 0.4f)));
            gameObject.GameObjectTag = "Bullet";

            DamageBullet bullet = new DamageBullet();
            bullet.SetSettings(direction, tag);
            gameObject.InitializeObjectScript(bullet);

            return gameObject;
        }
    }
}

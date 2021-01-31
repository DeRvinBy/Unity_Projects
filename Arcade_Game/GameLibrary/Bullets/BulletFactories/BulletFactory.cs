using EngineLibrary.ObjectComponents;
using SharpDX;

namespace GameLibrary.Bullets.BulletFactories
{
    /// <summary>
    /// Абстрактный класс фабрики создания пуль 
    /// </summary>
    public abstract class BulletFactory
    {
        /// <summary>
        /// Создание игрового объекта пули
        /// </summary>
        /// <param name="position">Позиция появления пули</param>
        /// <param name="direction">Направление пули</param>
        /// <param name="tag">Тег игрового объекта, создающий пулю</param>
        /// <returns>Игровой объект</returns>
        public abstract GameObject CreateBullet(Vector2 position, Vector2 direction, string tag);
    }
}

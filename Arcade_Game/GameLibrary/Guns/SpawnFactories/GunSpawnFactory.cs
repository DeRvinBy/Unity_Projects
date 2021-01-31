using EngineLibrary.ObjectComponents;
using SharpDX;

namespace GameLibrary.Guns.SpawnFactories
{
    /// <summary>
    /// Фбстрактная фабрика создания места подбираемого оружия
    /// </summary>
    public abstract class GunSpawnFactory
    {
        /// <summary>
        /// Создание места подбираемого оружия
        /// </summary>
        /// <param name="position">Позиция появления</param>
        /// <returns>Игровой объект</returns>
        public abstract GameObject CreateGunSpawn(Vector2 position);
    }
}

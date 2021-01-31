using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Maze;
using SharpDX;

namespace GameLibrary.Guns.SpawnFactories
{
    /// <summary>
    /// Фабрика создания места подбираемого оружия замедления
    /// </summary>
    public class SlowdownGunSpawnFactory : GunSpawnFactory
    {
        /// <summary>
        /// Создание места подбираемого оружия замедления
        /// </summary>
        /// <param name="position">Позиция появления</param>
        /// <returns>Игровой объект</returns>
        public override GameObject CreateGunSpawn(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(position, new Size2F(1, 1)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/MazeElements/Spawn Guns/slowdown spawn 1.png")));
            gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(0.8f, 0.8f)));
            gameObject.GameObjectTag = "Spawn";

            GunSpawn gunSpawn = new GunSpawn();
            DamageGun damageGun = new DamageGun();
            SlowdownGun slowdownGun = new SlowdownGun();
            slowdownGun.SetDecoratedGun(damageGun);

            gunSpawn.InitializeGunSpawn("slowdown", slowdownGun, 20f);

            gameObject.InitializeObjectScript(gunSpawn);

            return gameObject;
        }
    }
}

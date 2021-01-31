using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Guns.SpawnFactories;
using SharpDX;
using System;

namespace GameLibrary.Maze
{
    /// <summary>
    /// Класс игрового менеджера
    /// </summary>
    public class SpawnManager : ObjectScript
    {
        private const float timeToSpawn = 5f;
        private float currentTimeToSpawn;

        private MazeScene maze;
        private GunSpawnFactory spawnFactory;

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        public override void Start()
        {
            maze = MazeScene.instance;
            currentTimeToSpawn = Time.CurrentTime;
        }

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        public override void Update()
        {
            if (currentTimeToSpawn < Time.CurrentTime)
            {
                Random random = new Random();
                int chance = random.Next(0, 101);

                Vector2 position = maze.GetRandomPosition();

                if (chance <= 20)
                {
                    spawnFactory = new DamageGunSpawnFactory();
                }
                else if (chance > 20 && chance <= 50)
                {
                    spawnFactory = new FrezzeGunSpawnFactory();
                }
                else
                {
                    spawnFactory = new SlowdownGunSpawnFactory();
                }

                maze.AddObjectOnScene(spawnFactory.CreateGunSpawn(position));

                currentTimeToSpawn += timeToSpawn;
            }
        }
    }
}

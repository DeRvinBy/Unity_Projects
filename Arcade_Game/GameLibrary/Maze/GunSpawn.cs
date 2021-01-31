using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Guns;

namespace GameLibrary.Maze
{
    /// <summary>
    /// Класс подбираемого оружия в лабиринте
    /// </summary>
    public class GunSpawn : ObjectScript
    {
        private MazeScene maze;
        private Gun dropOutGun;
        private float cuurentTimeToDisappear;
        private string nameOfgun;

        /// <summary>
        /// Инициализация места подбираемого оружия
        /// </summary>
        /// <param name="name">Название оружия</param>
        /// <param name="gun">Подбираемое оружие</param>
        /// <param name="disappearTime">Время исчезнование места</param>
        public void InitializeGunSpawn(string name, Gun gun, float disappearTime)
        {
            dropOutGun = gun;
            nameOfgun = name;
            cuurentTimeToDisappear = Time.CurrentTime + disappearTime;
        }

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        public override void Start()
        {
            maze = MazeScene.instance;

            Animation animation = new Animation(RenderingSystem.LoadAnimation("Resources/MazeElements/Spawn Guns/" + nameOfgun + " spawn ", 2), 0.3f, true);
            gameObject.Sprite.AddAnimation("idle", animation);

            gameObject.Sprite.SetAnimation("idle");
        }

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        public override void Update()
        {
            if(cuurentTimeToDisappear < Time.CurrentTime)
            {
                maze.RemoveObjectFromScene(gameObject);
            }

            if(gameObject.Collider.CheckIntersection(out GameObject player,"Blue Player","Red Player"))
            {
                if(player.GameObjectTag == "Blue Player")
                    maze.AddObjectOnScene(maze.BluePlayerFactory.CreateGun(dropOutGun, nameOfgun));
                else if (player.GameObjectTag == "Red Player")
                    maze.AddObjectOnScene(maze.RedPlayerFactory.CreateGun(dropOutGun, nameOfgun));

                maze.RemoveObjectFromScene(gameObject);
            }
        }
    }
}

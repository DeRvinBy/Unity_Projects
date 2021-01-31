using EngineLibrary.ObjectComponents;
using GameLibrary.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Maze
{
    /// <summary>
    /// Класс монеты
    /// </summary>
    public class Coin : ObjectScript
    {
        private MazeScene maze;

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        public override void Start()
        {
            maze = MazeScene.instance;
        }

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        public override void Update()
        {
            if(gameObject.Collider.CheckIntersection(out Player player))
            {
                player.ChangeCoinsValue(1);
                maze.RemoveObjectFromScene(gameObject);
            }
        }
    }
}

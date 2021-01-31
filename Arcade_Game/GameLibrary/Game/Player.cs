using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using SharpDX;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Maze;

namespace GameLibrary.Game
{
    /// <summary>
    /// Класс, описывающий сценарий поведения игрока
    /// </summary>
    public class Player : ObjectScript
    {
        /// <summary>
        /// Управление игрока
        /// </summary>
        public PlayerControl Control { get; private set; }
        /// <summary>
        /// Скорость игрока
        /// </summary>
        public float Speed { get; set; } = 5;
        /// <summary>
        /// Возможность двигаться у игрока
        /// </summary>
        public bool IsCanMove { get; set; } = true;
        /// <summary>
        /// Собарнные монеты
        /// </summary>
        public int Coins { get; private set; } = 0;

        private GameObject childGameObject;

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        public override void Start()
        {
            Animation animation;

            if (gameObject.GameObjectTag == "Blue Player")
            {
                animation = new Animation(RenderingSystem.LoadAnimation("Resources/Blue Player/left idle ", 2), 0.2f, true);
                gameObject.Sprite.AddAnimation("idleLeft", animation);
                animation = new Animation(RenderingSystem.LoadAnimation("Resources/Blue Player/left run ", 4), 0.2f, true);
                gameObject.Sprite.AddAnimation("runLeft", animation);
                animation = new Animation(RenderingSystem.LoadAnimation("Resources/Blue Player/right idle ", 2), 0.2f, true);
                gameObject.Sprite.AddAnimation("idleRight", animation);
                animation = new Animation(RenderingSystem.LoadAnimation("Resources/Blue Player/right run ", 4), 0.2f, true);
                gameObject.Sprite.AddAnimation("runRight", animation);

                Control = new PlayerControl(AxisOfInput.Horizontal, AxisOfInput.Vertical, Key.Space);
            }
            else
            {
                animation = new Animation(RenderingSystem.LoadAnimation("Resources/Red Player/left idle ", 2), 0.2f, true);
                gameObject.Sprite.AddAnimation("idleLeft", animation);
                animation = new Animation(RenderingSystem.LoadAnimation("Resources/Red Player/left run ", 4), 0.2f, true);
                gameObject.Sprite.AddAnimation("runLeft", animation);
                animation = new Animation(RenderingSystem.LoadAnimation("Resources/Red Player/right idle ", 2), 0.2f, true);
                gameObject.Sprite.AddAnimation("idleRight", animation);
                animation = new Animation(RenderingSystem.LoadAnimation("Resources/Red Player/right run ", 4), 0.2f, true);
                gameObject.Sprite.AddAnimation("runRight", animation);

                Control = new PlayerControl(AxisOfInput.AlternativeHorizontal, AxisOfInput.AlternativeVertical, Key.NumberPadEnter);
            }

            gameObject.Sprite.SetAnimation("idleLeft");
        }

        /// <summary>
        /// Установка дочернего объекта 
        /// </summary>
        /// <param name="gameObject">Дочерний объект</param>
        public void SetChildGameObject(GameObject gameObject)
        {
            if (childGameObject != null)
                MazeScene.instance.RemoveObjectFromScene(childGameObject);

            childGameObject = gameObject;
        }

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        public override void Update()
        {
            if (gameObject.IsActive && IsCanMove)
                Move();     
        }

        /// <summary>
        /// Изменение значени монет
        /// </summary>
        /// <param name="value">Значение, которое прибавляется к текущему значению монет</param>
        public void ChangeCoinsValue(int value)
        {
            Coins += value;
            GameEvents.ChangeCoins?.Invoke(gameObject.GameObjectTag, Coins);
        }

        /// <summary>
        /// Метод движения игрока
        /// </summary>
        private void Move()
        {
            int directionX = 0, directionY = 0;

            directionX = Input.GetAxis(Control.HorizontalAxis);

            if (gameObject.Collider.CheckIntersection("Stair"))
            {
                directionY = Input.GetAxis(Control.VerticalAxis);
                gameObject.Transform.IsUseGravitation = gameObject.Collider.CheckIntersection("Wall");
            }
            else
            {
                gameObject.Transform.IsUseGravitation = true;
            }

            Vector2 direction;

            if (directionX == 0)
            {
                if(childGameObject != null)
                    childGameObject.Sprite.IsFlipX = gameObject.Sprite.IsFlipX;

                if (childGameObject != null && childGameObject.Sprite.IsFlipX)
                    childGameObject.Sprite.SetAnimation("idleLeft");
                else if (childGameObject != null)
                    childGameObject.Sprite.SetAnimation("idleRight");

                if (gameObject.Sprite.IsFlipX)
                    gameObject.Sprite.SetAnimation("idleLeft");
                else
                    gameObject.Sprite.SetAnimation("idleRight");

                direction = new Vector2(0, directionY);
            }
            else
            {
                gameObject.Sprite.IsFlipX = directionX < 0;

                if (childGameObject != null)
                    childGameObject.Sprite.IsFlipX = gameObject.Sprite.IsFlipX;

                if (childGameObject != null && childGameObject.Sprite.IsFlipX)
                    childGameObject.Sprite.SetAnimation("runLeft");
                else if (childGameObject != null)
                    childGameObject.Sprite.SetAnimation("runRight");

                if (gameObject.Sprite.IsFlipX)
                    gameObject.Sprite.SetAnimation("runLeft");
                else
                    gameObject.Sprite.SetAnimation("runRight");

                direction = new Vector2(directionX, 0);
            }

            Vector2 movement = direction * Speed * Time.DeltaTime;
            gameObject.Transform.SetMovement(movement);

            DetectCollision();
        }

        /// <summary>
        /// Распознавание столкновений и реакция на них
        /// </summary>
        private void DetectCollision()
        {
            if (gameObject.Collider.CheckIntersection("Wall","BreakWall"))
            {
                gameObject.Transform.ResetMovement();
            }

            if (gameObject.GameObjectTag == "Blue Player" || gameObject.GameObjectTag == "Red Player")
                gameObject.Transform.AddGravitation();

            string tag = (Input.GetAxis(Control.VerticalAxis) == -1) ? "" : "Platform";

            if (gameObject.Collider.CheckIntersection("Wall", tag))
            {
                gameObject.Transform.ResetGravitation();
            }
        }
    }

    /// <summary>
    /// Структура игрового управления персонажа
    /// </summary>
    public struct PlayerControl
    {
        /// <summary>
        /// Горизонтальная ось передвижения
        /// </summary>
        public AxisOfInput HorizontalAxis { get; private set; }
        /// <summary>
        /// Вертикальная ось передвижения
        /// </summary>
        public AxisOfInput VerticalAxis { get; private set; }
        /// <summary>
        /// Кнопка стрельбы
        /// </summary>
        public Key ShootKey { get; private set; }

        /// <summary>
        /// Конструктор структуры
        /// </summary>
        /// <param name="horizontalAxis">Горизонтальная ось передвижения</param>
        /// <param name="verticalAxis"> Вертикальная ось передвижения</param>
        /// <param name="shootKey">Кнопка стрельбы</param>
        public PlayerControl(AxisOfInput horizontalAxis, AxisOfInput verticalAxis, Key shootKey)
        {
            HorizontalAxis = horizontalAxis;
            VerticalAxis = verticalAxis;
            ShootKey = shootKey;
        }
    }
}

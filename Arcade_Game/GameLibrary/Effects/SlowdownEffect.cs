using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Game;

namespace GameLibrary.Effects
{
    /// <summary>
    /// Класс эффекта замедления
    /// </summary>
    public class SlowdownEffect : EffectDecorator
    {
        /// <summary>
        /// Экземпляр текущего эффекта на сцене для синего игрока
        /// </summary>
        public static SlowdownEffect BluePlayerEffect { get; private set; } = null;
        /// <summary>
        /// Экземпляр текущего эффекта на сцене для красного игрока
        /// </summary>
        public static SlowdownEffect RedPlayerEffect { get; private set; } = null;
        /// <summary>
        /// Время дейтсвия эффекта
        /// </summary>
        public override float EffectTime => decoratedEffect.EffectTime + 5f;

        private float trueSpeed;

        /// <summary>
        /// Активация эффекта
        /// </summary>
        /// <param name="player">Игровой объект, на который будет наложен эффект</param>
        public override void ActivateEffect(GameObject player)
        {
            playerGameObject = player;
            trueSpeed = (playerGameObject.Script as Player).Speed;
            (playerGameObject.Script as Player).Speed = trueSpeed / 2;

            if (playerGameObject.GameObjectTag == "Blue Player")
                BluePlayerEffect = this;
            else
                RedPlayerEffect = this;

            GameEvents.ChangeEffect?.Invoke(playerGameObject.GameObjectTag, "Slowdown");
        }

        /// <summary>
        /// Поведение на сцене
        /// </summary>
        protected override void BehaviorOnScene()
        {
            if (playerGameObject.Sprite.IsFlipX)
                gameObject.Sprite.SetAnimation("idleLeft");
            else
                gameObject.Sprite.SetAnimation("idleRight");
        }

        /// <summary>
        /// Деактивация эффекта
        /// </summary>
        protected override void DeactivateEffect()
        {
            (playerGameObject.Script as Player).Speed = trueSpeed;

            if (playerGameObject.GameObjectTag == "Blue Player")
                BluePlayerEffect = null;
            else
                RedPlayerEffect = null;

            maze.RemoveObjectFromScene(gameObject);

            GameEvents.ChangeEffect?.Invoke(playerGameObject.GameObjectTag, "");
        }

        /// <summary>
        /// Инициализация эффекта
        /// </summary>
        protected override void Initialize()
        {
            Animation animation = new Animation(RenderingSystem.LoadAnimation("Resources/MazeElements/Effects/slowdown left idle ", 2), 0.2f, true);
            gameObject.Sprite.AddAnimation("idleLeft", animation);
            animation = new Animation(RenderingSystem.LoadAnimation("Resources/MazeElements/Effects/slowdown right idle ", 2), 0.2f, true);
            gameObject.Sprite.AddAnimation("idleRight", animation);

            if(playerGameObject.Sprite.IsFlipX)
                gameObject.Sprite.SetAnimation("idleLeft");
            else
                gameObject.Sprite.SetAnimation("idleRight");
        }
    }
}

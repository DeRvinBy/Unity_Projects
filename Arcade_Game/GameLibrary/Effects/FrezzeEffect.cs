using System.Collections.Generic;
using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using GameLibrary.Game;
using SharpDX.Direct2D1;

namespace GameLibrary.Effects
{
    /// <summary>
    /// Эффект заморозки
    /// </summary>
    public class FrezzeEffect : EffectDecorator
    {
        /// <summary>
        /// Экземпляр текущего эффекта на сцене для синего игрока
        /// </summary>
        public static FrezzeEffect BluePlayerEffect { get; private set; } = null;
        /// <summary>
        /// Экземпляр текущего эффекта на сцене для красного игрока
        /// </summary>
        public static FrezzeEffect RedPlayerEffect { get; private set; } = null;
        /// <summary>
        /// Время дейтсвия эффекта
        /// </summary>
        public override float EffectTime => decoratedEffect.EffectTime + 2f;

        /// <summary>
        /// Активация эффекта
        /// </summary>
        /// <param name="player">Игровой объект, на который будет наложен эффект</param>
        public override void ActivateEffect(GameObject player)
        {
            playerGameObject = player;
            (playerGameObject.Script as Player).IsCanMove = false;

            if (playerGameObject.GameObjectTag == "Blue Player")
                BluePlayerEffect = this;
            else
                RedPlayerEffect = this;

            GameEvents.ChangeEffect?.Invoke(playerGameObject.GameObjectTag, "Frezze");
        }

        /// <summary>
        /// Поведение на сцене
        /// </summary>
        protected override void BehaviorOnScene() { }

        /// <summary>
        /// Деактивация эффекта
        /// </summary>
        protected override void DeactivateEffect()
        {
            (playerGameObject.Script as Player).IsCanMove = true;

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
            Animation animation = new Animation(new List<Bitmap>() { RenderingSystem.LoadBitmap("Resources/MazeElements/Effects/frezze left idle.png") }, 1f, true);
            gameObject.Sprite.AddAnimation("idleLeft", animation);
            animation = new Animation(new List<Bitmap>() { RenderingSystem.LoadBitmap("Resources/MazeElements/Effects/frezze right idle.png") }, 1f, true);
            gameObject.Sprite.AddAnimation("idleRight", animation);

            if (playerGameObject.Sprite.IsFlipX)
                gameObject.Sprite.SetAnimation("idleLeft");
            else
                gameObject.Sprite.SetAnimation("idleRight");
        }
    }
}

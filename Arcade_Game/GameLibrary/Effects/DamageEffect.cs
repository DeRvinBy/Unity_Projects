using EngineLibrary.ObjectComponents;
using GameLibrary.Game;

namespace GameLibrary.Effects
{
    /// <summary>
    /// Эффект после урона
    /// </summary>
    public class DamageEffect : Effect
    {
        /// <summary>
        /// Время дейтсвия эффекта
        /// </summary>
        public override float EffectTime => 5f;

        private int storedCoins;

        /// <summary>
        /// Активация эффекта
        /// </summary>
        /// <param name="player">Игровой объект, на который будет наложен эффект</param>
        public override void ActivateEffect(GameObject player)
        {
            playerGameObject = player;
            playerGameObject.IsActive = false;
            storedCoins = (playerGameObject.Script as Player).Coins / 2;
            (playerGameObject.Script as Player).ChangeCoinsValue(-storedCoins);

            GameEvents.ChangeEffect?.Invoke(playerGameObject.GameObjectTag, "Death");
        }

        /// <summary>
        /// Инициализация эффекта
        /// </summary>
        protected override void Initialize() { }

        /// <summary>
        /// Деактивация эффекта
        /// </summary>
        protected override void DeactivateEffect()
        {
            if (!playerGameObject.IsActive)
            {
                if (playerGameObject.GameObjectTag == "Blue Player")
                    playerGameObject.Transform.Position = maze.BluePlayerFactory.StartPosition;
                else
                    playerGameObject.Transform.Position = maze.RedPlayerFactory.StartPosition;
                playerGameObject.IsActive = true;
            }

            GameEvents.ChangeEffect?.Invoke(playerGameObject.GameObjectTag, "");
        }

        /// <summary>
        /// Поведение на сцене
        /// </summary>
        protected override void BehaviorOnScene()
        {
            if (gameObject.Sprite.Bitmap == null)
            {
                gameObject.Sprite.SetAnimation("idle");
            }

            if (gameObject.Collider.CheckIntersection(out Player player) && gameObject.IsActive)
            {
                player.ChangeCoinsValue(storedCoins);
                gameObject.IsActive = false;
            }

            if(playerGameObject.IsActive && !gameObject.IsActive)
            {
                maze.RemoveObjectFromScene(gameObject);
            }
        }
    }
}

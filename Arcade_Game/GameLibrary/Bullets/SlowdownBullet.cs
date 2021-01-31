using EngineLibrary.ObjectComponents;
using GameLibrary.Effects;
using GameLibrary.Effects.EffectFactories;

namespace GameLibrary.Bullets
{
    /// <summary>
    /// Класс замедляющей пули, декорируемый от пули урона
    /// </summary>
    public class SlowdownBullet : BulletDecorator
    {
        /// <summary>
        /// Скорость полёта
        /// </summary>
        public override float Speed => decoratedBullet.Speed + 10f;

        /// <summary>
        /// Взаимодействие с игроком
        /// </summary>
        /// <param name="playerGameObject">Игровой объект игрока</param>
        public override void PlayerInteraction(GameObject playerGameObject)
        {
            if (playerGameObject.GameObjectTag == "Blue Player" && SlowdownEffect.BluePlayerEffect != null) return;
            if (playerGameObject.GameObjectTag == "Red Player" && SlowdownEffect.RedPlayerEffect != null) return;

            SlowdownEffectFactory factory = new SlowdownEffectFactory();
            maze.AddObjectOnScene(factory.CreateEffect(playerGameObject));
        }
    }
}

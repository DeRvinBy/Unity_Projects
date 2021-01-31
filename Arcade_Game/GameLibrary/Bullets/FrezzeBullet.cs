using EngineLibrary.ObjectComponents;
using GameLibrary.Effects;
using GameLibrary.Effects.EffectFactories;

namespace GameLibrary.Bullets
{
    /// <summary>
    /// Класс замораживающей пули, декорируемый от пули урона
    /// </summary>
    public class FrezzeBullet : BulletDecorator
    {
        /// <summary>
        /// Скорость полёта
        /// </summary>
        public override float Speed => decoratedBullet.Speed + 5f;

        /// <summary>
        /// Взаимодействие с игроком
        /// </summary>
        /// <param name="playerGameObject">Игровой объект игрока</param>
        public override void PlayerInteraction(GameObject playerGameObject)
        {
            if (playerGameObject.GameObjectTag == "Blue Player" && FrezzeEffect.BluePlayerEffect != null) return;
            if (playerGameObject.GameObjectTag == "Red Player" && FrezzeEffect.RedPlayerEffect != null) return;

            FrezzeEffectFactory factory = new FrezzeEffectFactory();
            maze.AddObjectOnScene(factory.CreateEffect(playerGameObject));
        }
    }
}

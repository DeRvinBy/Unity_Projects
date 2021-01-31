using EngineLibrary.ObjectComponents;
using GameLibrary.Effects.EffectFactories;

namespace GameLibrary.Bullets
{
    /// <summary>
    /// Класс пули урона
    /// </summary>
    public class DamageBullet : Bullet
    {
        /// <summary>
        /// Скорость пули
        /// </summary>
        public override float Speed => 10f;

        /// <summary>
        /// Взаимодействие с игроком
        /// </summary>
        public override void PlayerInteraction(GameObject playerGameObject)
        {
            DamageEffectFactory factory = new DamageEffectFactory();
            maze.AddObjectOnScene(factory.CreateEffect(playerGameObject));
        }
    }
}

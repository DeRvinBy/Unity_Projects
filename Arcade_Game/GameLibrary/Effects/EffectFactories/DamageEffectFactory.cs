using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using SharpDX;

namespace GameLibrary.Effects.EffectFactories
{
    /// <summary>
    /// Фабрика создания эффекта смерти
    /// </summary>
    public class DamageEffectFactory : EffectFactory
    {
        /// <summary>
        /// Создание могилы с монетами
        /// </summary>
        /// <param name="player">Игровой объект игрока</param>
        /// <returns>Игровой объект</returns>
        public override GameObject CreateEffect(GameObject player)
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(player.Transform.Position, new Size2F(1f, 1f)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/MazeElements/Effects/damage idle 1.png")));
            gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(0.8f, 0.8f)));
            gameObject.GameObjectTag = "Effect";
            DamageEffect damageEffect = new DamageEffect();
            damageEffect.ActivateEffect(player);
            gameObject.InitializeObjectScript(damageEffect);

            return gameObject;
        }
    }
}

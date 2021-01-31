using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using SharpDX;

namespace GameLibrary.Effects.EffectFactories
{
    /// <summary>
    /// Фабрика создания эффекта заморозки
    /// </summary>
    public class FrezzeEffectFactory : EffectFactory
    {
        /// <summary>
        /// Создание замораживающего эффекта
        /// </summary>
        /// <param name="player">Игровой объект игрока</param>
        /// <returns>Игровой объект</returns>
        public override GameObject CreateEffect(GameObject player)
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(new Vector2(0, 0), new Size2F(1f, 1f)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/MazeElements/Effects/frezze left idle.png")));
            gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(0.8f, 0.8f)));
            gameObject.GameObjectTag = "Effect";
            gameObject.ParentGameObject = player;

            DamageEffect damageEffect = new DamageEffect();
            FrezzeEffect frezzeEffect = new FrezzeEffect();
            frezzeEffect.SetDecoratedEffect(damageEffect);
            frezzeEffect.ActivateEffect(player);

            gameObject.InitializeObjectScript(frezzeEffect);

            return gameObject;
        }
    }
}

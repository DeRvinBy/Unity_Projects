using EngineLibrary.EngineComponents;
using EngineLibrary.ObjectComponents;
using SharpDX;

namespace GameLibrary.Effects.EffectFactories
{
    /// <summary>
    /// Фабрика создания эффекта замедления
    /// </summary>
    public class SlowdownEffectFactory : EffectFactory
    {
        /// <summary>
        /// Создание замедляющего эффекта
        /// </summary>
        /// <param name="player">Игровой объект игрока</param>
        /// <returns>Игровой объект</returns>
        public override GameObject CreateEffect(GameObject player)
        {
            GameObject gameObject = new GameObject();
            gameObject.InitializeObjectComponent(new TransformComponent(new Vector2(0, 0), new Size2F(1f, 1f)));
            gameObject.InitializeObjectComponent(new SpriteComponent(RenderingSystem.LoadBitmap("Resources/MazeElements/Effects/slowdown left idle 1.png")));
            gameObject.InitializeObjectComponent(new ColliderComponent(gameObject, new Size2F(0.8f, 0.8f)));
            gameObject.GameObjectTag = "Effect";
            gameObject.ParentGameObject = player;

            DamageEffect damageEffect = new DamageEffect();
            SlowdownEffect slowdownEffect = new SlowdownEffect();
            slowdownEffect.SetDecoratedEffect(damageEffect);
            slowdownEffect.ActivateEffect(player);

            gameObject.InitializeObjectScript(slowdownEffect);

            return gameObject;
        }
    }
}

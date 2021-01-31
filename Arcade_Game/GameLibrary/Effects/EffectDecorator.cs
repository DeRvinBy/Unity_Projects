namespace GameLibrary.Effects
{
    /// <summary>
    /// Абстрактный класс декоратора эффекта
    /// </summary>
    public abstract class EffectDecorator : Effect
    {
        /// <summary>
        /// Время дейтсвия эффекта
        /// </summary>
        public override float EffectTime => decoratedEffect.EffectTime;
        /// <summary>
        /// Дерорируемый эффект
        /// </summary>
        protected DamageEffect decoratedEffect;

        /// <summary>
        /// Установка декорируемого эффекта
        /// </summary>
        /// <param name="effect">Декорируемый эффект</param>
        public void SetDecoratedEffect(DamageEffect effect)
        {
            decoratedEffect = effect;
        }
        /// <summary>
        /// Инициализация эффекта
        /// </summary>
        protected override void BehaviorOnScene() { }
        /// <summary>
        /// Деактивация эффекта
        /// </summary>
        protected override void DeactivateEffect() { }
        /// <summary>
        /// Поведение на сцене
        /// </summary>
        protected override void Initialize() { }
    }
}

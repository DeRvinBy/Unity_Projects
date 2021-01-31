using EngineLibrary.ObjectComponents;

namespace GameLibrary.Bullets
{
    /// <summary>
    /// Абстрактный класс декоратора пули
    /// </summary>
    public class BulletDecorator : Bullet
    {
        /// <summary>
        /// Скорость пули
        /// </summary>
        public override float Speed => decoratedBullet.Speed;
        /// <summary>
        /// Декорируемая пуля
        /// </summary>
        protected Bullet decoratedBullet;

        /// <summary>
        /// Установление декорируемой пули
        /// </summary>
        /// <param name="bullet">Пуля</param>
        public void SetDecoratedBullet(Bullet bullet)
        {
            decoratedBullet = bullet;
        }
        /// <summary>
        /// Взаимодействие с игроком
        /// </summary>
        public override void PlayerInteraction(GameObject playerGameObject) { }
    }
}

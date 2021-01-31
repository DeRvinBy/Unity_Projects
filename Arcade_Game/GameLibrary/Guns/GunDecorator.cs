using SharpDX;

namespace GameLibrary.Guns
{
    /// <summary>
    /// Абстрактный класс декоратора оружия
    /// </summary>
    public abstract class GunDecorator : Gun
    {
        /// <summary>
        /// Время использования оружия
        /// </summary>
        public override float UseTime => decoratedGun.UseTime;
        /// <summary>
        /// Время перезарядки оружия
        /// </summary>
        public override float ReloadTime => decoratedGun.ReloadTime;
        /// <summary>
        /// Декорируемое оружие
        /// </summary>
        protected Gun decoratedGun;

        /// <summary>
        /// Установление декорируемого оружия
        /// </summary>
        /// <param name="gun">Оружие</param>
        public void SetDecoratedGun(Gun gun)
        {
            decoratedGun = gun;
        }

        /// <summary>
        /// Загрузка анимации оружия
        /// </summary>
        protected override void LoadAnimation() { }

        /// <summary>
        /// Создание пули из фабрики
        /// </summary>
        /// <param name="position">Позиция создания</param>
        /// <param name="direction">Направление пули</param>
        protected override void SpawnBullet(Vector2 position, Vector2 direction) { }
    }
}

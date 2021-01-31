using UnityEngine;
using Scripts.Pool;

namespace Scripts.Game.EnemyObjects
{
    public class BulletEnemyShooting : EnemyShooting
    {
        [Header("Prehabs")]

        [SerializeField]
        [Tooltip("Prehab of bullet")]
        private GameObject BulletPrehab;

        [Header("ShootOption")]
        [SerializeField]
        [Tooltip("Angle bullet")]
        private int AngleBullet = 45;

        public override bool Shoot()
        {
            Quaternion quaternion = Quaternion.Euler(0, 0, _transform.eulerAngles.z);
            PoolManager.GetFromPool(BulletPrehab, _transform.position, quaternion);

            quaternion = Quaternion.Euler(0, 0, _transform.eulerAngles.z - AngleBullet);
            PoolManager.GetFromPool(BulletPrehab, _transform.position, quaternion);

            quaternion = Quaternion.Euler(0, 0, _transform.eulerAngles.z + AngleBullet);
            PoolManager.GetFromPool(BulletPrehab, _transform.position, quaternion);

            return true;
        }
    }
}
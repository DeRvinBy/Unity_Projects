using UnityEngine;
using Scripts.Pool;

namespace Scripts.Game.EnemyObjects
{
    public class BombEnemyShooting : EnemyShooting
    {
        [Header("Prehabs")]

        [SerializeField]
        [Tooltip("Prehab of bomb")]
        private GameObject BombPrehab;

        public override bool Shoot()
        {
            Quaternion quaternion = Quaternion.Euler(0, 0, _transform.eulerAngles.z);
            PoolManager.GetFromPool(BombPrehab, _transform.position, quaternion);

            return true;
        }
    }
}
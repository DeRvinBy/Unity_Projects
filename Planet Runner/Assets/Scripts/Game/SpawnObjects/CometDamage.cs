using UnityEngine;
using Scripts.Pool;
using Scripts.Game.Player;
using Scripts.Game.EnemyObjects;
using Random = UnityEngine.Random;
using Scripts.Game.Space;

namespace Scripts.Game.SpawnObjects
{
    [RequireComponent(typeof(CometFly))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class CometDamage : ObjectDamage
    {
        [Header("Spawn after destroy")]

        [SerializeField]
        [Tooltip("Exsplosion after destroy comet")]
        [Range(0,100)] private int ChanceOfSpawnStone = 20;

        [Header("Prehabs")]

        [SerializeField]
        [Tooltip("SpaceStone after destroy comet")]
        private GameObject SpaceStonePrehab;

        [SerializeField]
        [Tooltip("Exsplosion after destroy comet")]
        private GameObject ExsplosionPrehab;

        public override void InteractionWithObject(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerStats player))
            {
                player.ChangeLife(-Damage);
            }
            else if (collision.TryGetComponent(out Planet planet))
            {
                int chance = Random.Range(0, 101);

                if (chance < ChanceOfSpawnStone)
                    PoolManager.GetFromPool(SpaceStonePrehab, _transform.position, _transform.rotation, Planet._transform);
            }
            else if(collision.TryGetComponent(out Enemy enemy))
            {
                enemy.DestroyEnemy();
            }

            if (collision.TryGetComponent(out Gravitation gravi)) return;

            DestroySpaceObject();
        }

        protected override void DestroySpaceObject()
        {
            PoolManager.GetFromPool(ExsplosionPrehab, _transform.position, _transform.rotation, Planet._transform);
            base.DestroySpaceObject();
        }
    }
}
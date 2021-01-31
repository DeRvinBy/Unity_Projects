using System.Collections;
using UnityEngine;
using Scripts.Pool;
using Scripts.Game.Space;

namespace Scripts.Game.SpawnObjects
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(BombFly))]
    public class BombDamage : ObjectDamage
    {
        [Header("Fly component")]

        [SerializeField]
        [Tooltip("Component of fly")]
        private BombFly Fly;

        [Header("Options of Explosion")]

        [SerializeField]
        [Tooltip("Time to explosion bomb")]
        private float TimeExplosion;

        [SerializeField]
        [Tooltip("Exsplosion after destroy comet")]
        private GameObject ExsplosionPrehab;

        public override void InteractionWithObject(Collider2D collision)
        {
            if (collision.TryGetComponent(out Planet planet))
            {
                Fly.StopMove();
                _transform.SetParent(Planet._transform);
                StartCoroutine(WaitForDestroy());
                return;
            }
        }

        protected override void DestroySpaceObject()
        {
            PoolManager.GetFromPool(ExsplosionPrehab, _transform.position, _transform.rotation, Planet._transform);
            base.DestroySpaceObject();
        }

        IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(TimeExplosion);

            DestroySpaceObject();
        }
    }
}
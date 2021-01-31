using UnityEngine;
using System.Collections;
using Scripts.Pool;

namespace Scripts.Game.EnemyObjects
{
    public class Enemy : MonoBehaviour
    {
        [Header("EnemyComponents")]

        [SerializeField]
        [Tooltip("Move component of Enemy")]
        private EnemyMovement Movement;

        [SerializeField]
        [Tooltip("Shoot component of Enemy")]
        private EnemyShooting Shooting;

        [Header("Prehabs")]

        [SerializeField]
        [Tooltip("Exsplosion after destroy comet")]
        private GameObject ExsplosionPrehab;

        [Header("ShootOption")]

        [SerializeField]
        [Tooltip("Time to charge shoot")]
        private float TimeOfChargShootTime = 1f;

        [SerializeField]
        [Tooltip("Time to reload gun")]
        private float ReloadShootTime = 1f;

        private Transform _transform;
        private bool _isActive = false;

        private void Start()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _isActive = true;
            StartCoroutine(EnemyAction());
        }

        IEnumerator EnemyAction()
        {
            yield return new WaitForSeconds(TimeOfChargShootTime);

            while (_isActive)
            {
                yield return new WaitUntil(() => Movement.Move());

                yield return new WaitForSeconds(TimeOfChargShootTime);

                yield return new WaitUntil(() => Shooting.Shoot());

                yield return new WaitForSeconds(ReloadShootTime);
            }           
        }

        public void DestroyEnemy()
        {
            _isActive = false;
            PoolManager.GetFromPool(ExsplosionPrehab, _transform.position, _transform.rotation);
            PoolManager.ReturnToPool(gameObject);
        }
    }
}
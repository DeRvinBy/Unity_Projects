using Scripts.Pool;
using UnityEngine;

namespace Scripts.Game.SpawnObjects
{
    public abstract class ObjectDamage : MonoBehaviour
    {
        [Header("Damage options")]

        [SerializeField]
        [Tooltip("Damage to the player")]
        protected int Damage = 1;

        protected Transform _transform;

        protected void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            InitializeOnEnable();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            InteractionWithObject(collision);
        }

        protected virtual void InitializeOnEnable() { }

        public abstract void InteractionWithObject(Collider2D collision);
        protected virtual void DestroySpaceObject()
        {
            PoolManager.ReturnToPool(gameObject);
        }
    }
}
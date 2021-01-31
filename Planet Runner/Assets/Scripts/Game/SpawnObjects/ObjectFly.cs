using UnityEngine;

namespace Scripts.Game.SpawnObjects
{
    public abstract class ObjectFly : MonoBehaviour
    {
        [Header("Move options")]

        [SerializeField]
        [Tooltip("Start move speed")]
        protected float StartMoveSpeed = 10f;

        protected float _moveSpeed;
        protected Transform _transform;

        protected void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            InitializeOnEnable();
        }

        private void Update()
        {
            Move();
        }

        protected abstract void Move();

        public virtual void ChangeSpeed(float value)
        {
            _moveSpeed += value;
        }

        protected virtual void InitializeOnEnable()
        {
            _moveSpeed = StartMoveSpeed;
        }
    }
}

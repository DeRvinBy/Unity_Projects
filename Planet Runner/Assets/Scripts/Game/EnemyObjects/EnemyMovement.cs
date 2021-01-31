using UnityEngine;

namespace Scripts.Game.EnemyObjects
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        protected Transform _transform;

        private void Start()
        {
            Initializemovement();
        }

        protected virtual void Initializemovement()
        {
            _transform = transform;
        }

        public abstract bool Move();
    }
}
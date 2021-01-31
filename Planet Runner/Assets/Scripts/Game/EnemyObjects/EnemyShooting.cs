using UnityEngine;

namespace Scripts.Game.EnemyObjects
{
    public abstract class EnemyShooting : MonoBehaviour
    {
        protected Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public abstract bool Shoot(); 
    }
}
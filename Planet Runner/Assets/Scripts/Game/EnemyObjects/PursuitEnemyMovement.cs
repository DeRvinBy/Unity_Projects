using UnityEngine;
using Scripts.Game.Player;

namespace Scripts.Game.EnemyObjects
{
    public class PursuitEnemyMovement : EnemyMovement
    {
        [Header("MoveOptions")]

        [SerializeField]
        [Tooltip("Speed of Enemt Rotation")]
        private float RotationSpeed = 60f;

        [SerializeField]
        [Tooltip("Coefficient of Enemy Rotation")]
        private float CoefficientRotation = 0.02f;

        private Transform _playerTransform;

        protected override void Initializemovement()
        {
            base.Initializemovement();

            _playerTransform = PlayerMoving._transform;
        }

        public override bool Move()
        {
            if (_playerTransform == null) return true;

            float moveSpeed = RotationSpeed * CoefficientRotation;

            Vector3 enemyPosition = new Vector3(_transform.position.x, _transform.position.y, 0);
            _transform.position = Vector3.RotateTowards(enemyPosition, _playerTransform.position, moveSpeed * Time.deltaTime, 0);
            _transform.LookAt(Vector3.zero, Vector3.zero);
            _transform.up = transform.position - Vector3.zero;

            float delta = _playerTransform.eulerAngles.z - _transform.eulerAngles.z;

            return Mathf.Abs(delta) <= 0.5f;
        }
    }
}
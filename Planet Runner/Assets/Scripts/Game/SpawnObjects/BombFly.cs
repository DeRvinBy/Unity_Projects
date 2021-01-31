using UnityEngine;

namespace Scripts.Game.SpawnObjects
{
    [RequireComponent(typeof(BombDamage))]
    public class BombFly : ObjectFly
    {
        private bool _isMove;

        protected override void Move()
        {
            if (!_isMove) return;

            Vector3 direction = _transform.position - _transform.up;
            _transform.position = Vector3.MoveTowards(_transform.position, direction, _moveSpeed * Time.deltaTime);
        }

        public void StopMove()
        {
            _isMove = false;
        }

        protected override void InitializeOnEnable()
        {
            base.InitializeOnEnable();
            _isMove = true;
        }
    }
}
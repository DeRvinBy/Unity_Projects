using UnityEngine;

namespace Scripts.Game.SpawnObjects
{
    [RequireComponent(typeof(BulletDamage))]
    public class BulletFly : ObjectFly
    {
        protected override void Move()
        {
            Vector3 direction = _transform.position - _transform.up;
            _transform.position = Vector3.MoveTowards(_transform.position, direction, _moveSpeed * Time.deltaTime);
        }
    }
}
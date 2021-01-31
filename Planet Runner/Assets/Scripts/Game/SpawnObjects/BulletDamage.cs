using UnityEngine;
using Scripts.Game.Player;

namespace Scripts.Game.SpawnObjects
{
    [RequireComponent(typeof(BulletFly))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class BulletDamage : ObjectDamage
    {
        public override void InteractionWithObject(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerStats player))
            {
                player.ChangeLife(-Damage);
            }

            DestroySpaceObject();
        }
    }
}
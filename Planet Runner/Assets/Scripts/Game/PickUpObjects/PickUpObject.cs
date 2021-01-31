using UnityEngine;
using Scripts.Game.Player;
using Scripts.Pool;

namespace Scripts.Game.PickUpObjects
{
    public abstract class PickUpObject : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerStats player))
            {
                Action(player);
            }
        }

        protected abstract void Action(PlayerStats player);

        protected virtual void DestroyPuckUpObject()
        {
            PoolManager.ReturnToPool(gameObject);
        }
    }
}
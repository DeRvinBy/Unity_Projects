using UnityEngine;
using Scripts.Game.SpawnObjects;

namespace Scripts.Game.Space
{
    public class Gravitation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The value by which the speed increases or decreases")]
        private float ChangedSpeed = 5f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ObjectFly comet))
            {
                comet.ChangeSpeed(-ChangedSpeed);
            }
        }
    }
}
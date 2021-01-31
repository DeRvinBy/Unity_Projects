using UnityEngine;
using Scripts.Creature;

namespace Scripts.GameManagers
{
    public class TargetCamera : MonoBehaviour
    {
        public float moveTime = 10f;

        private Transform target;

        private void Start()
        {
            target = PlayerMove.instance.transform;
        }

        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, target.position, Time.deltaTime * 10f);
        }
    }
}
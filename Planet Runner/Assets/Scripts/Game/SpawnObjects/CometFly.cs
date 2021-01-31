using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.SpawnObjects
{
    [RequireComponent(typeof(CometDamage))]
    public class CometFly : ObjectFly
    {
        [Header("Size Spawn")]

        [SerializeField]
        [Tooltip("Minimum size of Spawn")]
        private float MinSize = 0.5f;

        [SerializeField]
        [Tooltip("Maximum size of Spawn")]
        private float MaxSize = 1f;

        protected override void InitializeOnEnable()
        {
            base.InitializeOnEnable();

            float angle = Random.Range(0, 361);
            _transform.RotateAround(Vector3.zero, Vector3.forward, angle);

            float size = Random.Range(MinSize, MaxSize);
            _transform.localScale = new Vector3(size, size, size);
        }

        protected override void Move()
        {
            _transform.position = Vector3.MoveTowards(_transform.position, Vector3.zero, _moveSpeed * Time.deltaTime);
        }
    }
}
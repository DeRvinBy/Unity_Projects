using UnityEngine;
using System;

namespace Scripts.Game.Space
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Planet : MonoBehaviour
    {
        [NonSerialized]
        public static Transform _transform = null;

        [Header("Options of planet")]

        [SerializeField]
        [Tooltip("Rotation speed in planet's orbit")]
        private float RotationSpeed = 10f;

        void Start()
        {
            if (_transform != null) Destroy(gameObject);

            _transform = transform;
        }

        void Update()
        {
            _transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
        }
    }
}
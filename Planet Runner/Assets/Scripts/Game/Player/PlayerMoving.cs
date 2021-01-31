using UnityEngine;
using System;

namespace Scripts.Game.Player
{
    public class PlayerMoving : MonoBehaviour
    {
        [NonSerialized]
        public static Transform _transform = null;

        [Header("SpriteRenderer componets")]

        [SerializeField]
        [Tooltip("SpriteRenderer component")]
        private SpriteRenderer SpriteRenderer; 

        [Header("Options of player's move")]

        [SerializeField]
        [Tooltip("Speed of Player rotation")]
        private float RotationSpeed = 50f;

        private float _movement;

        void Awake()
        {
            if (_transform != null) Destroy(gameObject);

            _transform = transform;
        }

        void Update()
        {
            _movement = - Input.GetAxis("Horizontal") * RotationSpeed;
            _transform.RotateAround(Vector3.zero, Vector3.forward,  _movement * Time.deltaTime);
            SpriteRenderer.flipX = _movement < 0;
        }
    }
}
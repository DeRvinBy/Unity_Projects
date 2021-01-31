using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Pool;
using Scripts.Game.Player;

namespace Scripts.Game.SpawnObjects
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class ExsplosionDamage : ObjectDamage
    {
        [Header("Optinons of sprites")]

        [SerializeField]
        [Tooltip("Variants of sprites")]
        private List<Sprite> Sprites;

        [Header("SpriteRenderer component")]

        [SerializeField]
        [Tooltip("Child SpriteRenderer")]
        private SpriteRenderer SpriteRenderer;

        private Animator _animator;

        private new void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        protected override void InitializeOnEnable()
        {
            if (Sprites.Count > 0)
                SpriteRenderer.sprite = Sprites[Random.Range(0, Sprites.Count)];

            float size = Random.Range(0.5f, 1.0f);
            
            _transform.localScale = new Vector3(size, size, size);

            StartCoroutine(WaitForDestroy());
        }

        public override void InteractionWithObject(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerStats player))
            {
                player.ChangeLife(-Damage);
            }
        }

        IEnumerator WaitForDestroy()
        {
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Stub"));
            PoolManager.ReturnToPool(gameObject);
        }
    }
}
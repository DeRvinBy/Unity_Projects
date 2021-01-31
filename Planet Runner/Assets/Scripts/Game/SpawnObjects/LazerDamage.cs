using System.Collections;
using UnityEngine;
using Scripts.Game.Player;

namespace Scripts.Game.SpawnObjects
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class LazerDamage : ObjectDamage
    {
        [Header("Options of lazer")]

        [SerializeField]
        [Tooltip("Time for decrease")]
        private float TimeDecrease = 2f; 

        private CapsuleCollider2D _collider;
        private Animator _animator;

        private int _decreaseAnimation = Animator.StringToHash("Decrease");

        private new void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<CapsuleCollider2D>();
        }

        protected override void InitializeOnEnable()
        {
            _collider.isTrigger = true;

            StartCoroutine(WaitForDestroy());
        }

        public override void InteractionWithObject(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerStats player))
                player.ChangeLife(-Damage);
        }

        IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(TimeDecrease);

            _collider.isTrigger = false;
            _animator.SetTrigger(_decreaseAnimation);

            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Stub"));

            gameObject.SetActive(false);
        }
    }
}
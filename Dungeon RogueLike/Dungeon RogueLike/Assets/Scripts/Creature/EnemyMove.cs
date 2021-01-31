using UnityEngine;
using System.Collections;
using Scripts.Items;
using Scripts.GameManagers;

namespace Scripts.Creature
{
    public class EnemyMove : MovingObjects, IObjectAction
    {
        [SerializeField] private int EnemyDamage;

        private Animator animator;
        private Transform targetPlayer;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            animator.SetTrigger("Restart");

            Managers.Game.AddEnemiesToList(this);
            targetPlayer = PlayerMove.instance.transform;

            boxCollider.enabled = true;
        }

        #region Move

        public void MoveEnemy()
        {
            if ((Random.Range(0, 2) == 0)) return;

            int horizontal = 0;
            int vertical = 0;

            if (Mathf.Abs(targetPlayer.position.x - transform.position.x) < float.Epsilon)
            {
                vertical = targetPlayer.position.y > transform.position.y ? 1 : -1;
            }
            else
            {
                horizontal = targetPlayer.position.x > transform.position.x ? 1 : -1;
                transform.rotation = targetPlayer.position.x > transform.position.x ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            }

            AttemptMove(horizontal, vertical);
        }

        protected override void AttemptMove(int xDir, int yDir)
        {
            base.AttemptMove(xDir, yDir);
        }

        protected override void OnCantMove(IObjectAction component)
        {
            if (component as PlayerMove)
            {
                Managers.Fight.Enemy = this;
                component.Action();
            }

            if (component as Box)
            {
                animator.SetTrigger("Attack");
                component.Action();
            }
        }

        #endregion

        #region Action

        public void Action()
        {
            Managers.Fight.StartFight();
        }

        public void Damage()
        {
            boxCollider.enabled = false;
            StartCoroutine(DeathAnim());
        }

        IEnumerator DeathAnim()
        {
            animator.SetTrigger("Death");
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("DeathStub"));
            gameObject.SetActive(false);
        }

        public void Attack()
        {
            animator.SetTrigger("Attack");
            PlayerMove.instance.Damage(EnemyDamage);
        }

        #endregion
    }
} 
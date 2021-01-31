using System.Collections;
using UnityEngine;
using Scripts.Items;
using Scripts.GameManagers;

namespace Scripts.Creature
{
    public class PlayerMove : MovingObjects, IObjectAction
    {
        public static PlayerMove instance = null;

        [Header("Energy")]
        [SerializeField] private int moveEnergy = 2;
        [SerializeField] private int attackEnergy = 2;
        [Header("Player Sprites")]
        [SerializeField] private SpriteRenderer spriteSword;
        [SerializeField] private SpriteRenderer spriteGetItem;

        private Animator animator;
        private Animator getItemAnimator;

        private Vector2 startTouchPoint = -Vector2.one;

        #region MonoBehaviour
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            animator = GetComponent<Animator>();
            getItemAnimator = spriteGetItem.GetComponent<Animator>();
        }

        private void Update()
        {
            if (!Managers.Game.IsPlayerTurn) return;

            int horizontal;
            int vertical;

            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");

            if (horizontal == -1)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else if (horizontal == 1)
                transform.rotation = Quaternion.Euler(0, 0, 0);

            if (horizontal != 0)
                vertical = 0;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                if(touch.phase == TouchPhase.Began)
                {
                    startTouchPoint = touch.position;
                }
                else if(touch.phase == TouchPhase.Ended && startTouchPoint.x >= 0)
                {
                    Vector2 endTouchPoint = touch.position;

                    float x = endTouchPoint.x - startTouchPoint.x;
                    float y = endTouchPoint.y - startTouchPoint.y;
                    startTouchPoint = -Vector2.one;

                    if(Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        horizontal = x > 0 ? 1 : -1;
                        transform.rotation = x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
                    }
                    else
                    {
                        vertical = y > 0 ? 1 : -1;
                    }

                }
            }

            if (horizontal != 0 || vertical != 0)
                AttemptMove(horizontal, vertical);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Exit") && !Managers.Game.IsPause)
            {
                Managers.Game.IsPause = true;
                StartCoroutine(DelayToExit());
            }
        }

        IEnumerator DelayToExit()
        {
            yield return new WaitForSeconds(moveTime);
            Managers.Board.RestartFloor();
        }
        #endregion

        #region Move
        protected override void AttemptMove(int xDir, int yDir)
        {    
            if (!Managers.Game.IsPlayerTurn) return;

            base.AttemptMove(xDir, yDir);

            if (isCanMove)
            {
                Managers.Game.IsPlayerTurn = false;
                Managers.Game.PlayEnemies();
                Managers.Game.ChangeEnergy(-moveEnergy);
            }
        }

        protected override void OnCantMove(IObjectAction component)
        {
            if (component is Box)
            {
                if (Managers.Fight.currentWeapon != null)
                {
                    Managers.Game.IsPlayerTurn = false;
                    Managers.Game.PlayEnemies();
                    Attack();
                    component.Action();
                }
                else
                {
                    Managers.UI.DisplayPopUpText("Нет оружия");
                }
            }

            if (component is EnemyMove)
            {
                Managers.Fight.Enemy = component as EnemyMove;
                component.Action();
            }

            if (component is Chest)
            {
                Managers.Game.IsPlayerTurn = false;
                Managers.Game.PlayEnemies();
                component.Action();
            }
        }

        #endregion

        #region Action
        public void Action()
        {
            Managers.Fight.StartFight();
        }

        public void SetWeapon(Sprite sprite)
        {
            spriteSword.sprite = sprite;
            GetItem(sprite);
        }

        public void GetItem(Sprite sprite)
        {
            spriteGetItem.sprite = sprite;
            getItemAnimator.SetTrigger("Play");
        }

        public void Attack()
        {
            animator.SetTrigger("Attack");
            Managers.Fight.ChangeWeaponStrenght();
            Managers.Game.ChangeEnergy(-attackEnergy);
        }

        public void Damage(int damage)
        {
            print("Damage" + Managers.Game.HealthPoints);
            animator.SetTrigger("Hit");
            Managers.Game.ChangeHealth(-damage);     
        }

        public void Death()
        {
            StartCoroutine(DeathAnim());
        }

        IEnumerator DeathAnim()
        {
            animator.SetTrigger("Death");
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("DeathStub"));
            yield return new WaitForSeconds(1f);
            Managers.Game.DiplayGameOver();
        }

        #endregion
    }
}
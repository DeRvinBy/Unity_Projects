using UnityEngine;
using Scripts.GameManagers;

namespace Scripts.Items
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int priceOfGold = 1;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            animator.SetTrigger("Restart");
            InvokeRepeating("PlayAnimation", Random.Range(2f, 5f), Random.Range(4f, 8f));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Managers.Game.ChangeGold(priceOfGold);
                gameObject.SetActive(false);
            }
        }

        private void PlayAnimation()
        {
            animator.SetTrigger("AnimCoin");
        }
    }
}
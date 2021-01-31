using System.Collections;
using UnityEngine;
using Scripts.GameManagers;

namespace Scripts.Items
{
    public class Box : MonoBehaviour, Creature.IObjectAction
    {
        private Animator animator;
        private int stage = 0;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            stage = 0;
            animator.SetInteger("Stage", stage);
        }

        public void Action()
        {
            animator.SetInteger("Stage", ++stage);

            if (stage == 3)
                StartCoroutine(DestroyBox());
        }

        IEnumerator DestroyBox()
        {
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("BoxIdleDestroyStub"));
            gameObject.SetActive(false);
        }
    }
}
using UnityEngine;
using System.Collections;

namespace Scripts.Creature
{
    public abstract class MovingObjects : MonoBehaviour
    {
        public float moveTime = .1f;
        public LayerMask blockingLayer;

        private float moveSpeed;
        protected BoxCollider2D boxCollider;
        protected bool isCanMove;

        protected void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            moveSpeed = 1 / moveTime;
        }

        protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector3 start = transform.position;
            Vector3 end = start + new Vector3(xDir, yDir, yDir/10f);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, blockingLayer);
            boxCollider.enabled = true;

            if (hit.transform != null)
                return false;

            StartCoroutine(Movement(end));
            return true;
        }

        IEnumerator Movement(Vector3 end)
        {
            float distance = (transform.position - end).sqrMagnitude;

            while(distance > float.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, end, moveSpeed * Time.deltaTime);
                distance = (transform.position - end).sqrMagnitude;
                yield return null;
            }
        }

        protected virtual void AttemptMove(int xDir, int yDir)
        {
            RaycastHit2D hit;
            isCanMove = Move(xDir, yDir, out hit);

            if (hit.transform == null)
                return;

            IObjectAction hitComponent = hit.transform.GetComponent<IObjectAction>();

            if (!isCanMove && hitComponent != null)
                OnCantMove(hitComponent);
        }

        protected abstract void OnCantMove(IObjectAction component);
    }
}
using System.Collections;
using UnityEngine;
using Scripts.Game.GameObjects;
using Scripts.Game.Player;

namespace Scripts.Game.Space
{
    public class EvucuationShip : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("GameManager Component")]
        private GameManager GameManager;

        private Animator _animator;
        private int _flyFormPlanetAnimation = Animator.StringToHash("Fly");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out PlayerStats player))
            {
                player.gameObject.SetActive(false);
                _animator.SetTrigger(_flyFormPlanetAnimation);
                StartCoroutine(FlyFromPlanet());
            }
        }

        IEnumerator FlyFromPlanet()
        {
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Stub"));

            GameManager.WinGame();
        }
    }
}
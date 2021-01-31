using UnityEngine;
using Scripts.Game.GameObjects;

namespace Scripts.Game.Player
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerStats : MonoBehaviour
    {
        [Header("Player's stats")]

        [SerializeField]
        [Tooltip("Count of life of player")]
        private int CountOfLife = 1;

        [SerializeField]
        [Tooltip("Count of life of player")]
        private int CountOfSpaceStone = 0;

        [Header("Game manager settings")]

        [SerializeField]
        [Tooltip("Game manager Component")]
        private GameManager GameManager;

        [SerializeField]
        [Tooltip("Value through which difficulty will increase")]
        private int ValueOfChangeDifficulty = 20;

        [SerializeField]
        [Tooltip("Value after which player can call evucuation")]
        private int ValueToCallEvucuation = 40;

        public void ChangeSpaceStone(int value)
        {
            CountOfSpaceStone += value;

            if (CountOfSpaceStone % ValueOfChangeDifficulty == 0)
                GameManager.ChangeDifficulty();

            if (CountOfSpaceStone == ValueToCallEvucuation)
                GameManager.ActivateEvacuationButton();

            GameManager.ChangeSpaceStoneText(CountOfSpaceStone);
        }

        public void ChangeLife(int value)
        {
            CountOfLife += value;

            if (CountOfLife < 1)
                DeathPlayer();
        }

        public void DeathPlayer()
        {
            GameManager.EndGame();
            gameObject.SetActive(false);
        }
    }
}
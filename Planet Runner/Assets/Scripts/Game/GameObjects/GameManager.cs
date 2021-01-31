using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Scripts.Pool;
using Scripts.Game.EnemyObjects;
using Scripts.Save;

namespace Scripts.Game.GameObjects
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance = null;

        [Header("UI componets")]

        [SerializeField]
        [Tooltip("Button to call evacuation")]
        private GameObject EvacuationButton;

        [SerializeField]
        [Tooltip("Text with value of space stone in game")]
        private TextMeshProUGUI SpaceStoneGameText;

        [Header("End game settings")]

        [SerializeField]
        [Tooltip("Pool cotainer with objects")]
        private PoolContainer PoolContainer;

        [SerializeField]
        [Tooltip("Spawner object")]
        private GameObject Spawner;

        [SerializeField]
        [Tooltip("Evucuation Ship GameObject")]
        private GameObject EvucuationShip;

        [SerializeField]
        [Tooltip("Lose animator GameObject")]
        private Animator LoseAnimator;

        [SerializeField]
        [Tooltip("Win animator GameObject")]
        private Animator WinAnimator;

        [SerializeField]
        [Tooltip("Text with value of space stone in menu")]
        private TextMeshProUGUI SpaceStoneMenuText;

        private int _valueOfSpaceStone;
        private int _activateAnimation = Animator.StringToHash("Activate");
        private UnityEvent _onDifficultyChange = new UnityEvent();

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        private void OnApplicationQuit()
        {
            _onDifficultyChange.RemoveAllListeners();
        }

        public void AddListener(UnityAction listener)
        {
            _onDifficultyChange.AddListener(listener);
        }

        public void ChangeDifficulty()
        {
            _onDifficultyChange.Invoke();
        }

        public void ActivateEvacuationButton()
        {
            EvacuationButton.SetActive(true);
        }

        public void ChangeSpaceStoneText(int value)
        {
            _valueOfSpaceStone = value;
            SpaceStoneGameText.text = value.ToString();
        }

        public void CallEvacuation()
        {
            _onDifficultyChange.RemoveAllListeners();
            Spawner.SetActive(false);

            Enemy[] enemies = PoolContainer.GetObjectsFromScene<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.DestroyEnemy();
            }

            EvucuationShip.SetActive(true);
        }

        public void WinGame()
        {
            SaveManager.LoadSave();
            SaveManager.Save.SpaceStone += _valueOfSpaceStone;
            SaveManager.SaveSave();

            Time.timeScale = 0;
            SpaceStoneMenuText.text = _valueOfSpaceStone.ToString();
            WinAnimator.SetTrigger(_activateAnimation);
        }

        public void EndGame()
        {
            Time.timeScale = 0;
            LoseAnimator.SetTrigger(_activateAnimation);
        }
    }
}
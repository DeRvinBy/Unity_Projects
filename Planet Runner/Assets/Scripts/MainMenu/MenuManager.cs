using TMPro;
using UnityEngine;
using Scripts.Save;
using UnityEngine.SceneManagement;

namespace Scripts.MainMenu
{
    [RequireComponent(typeof(BuyManager))]
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI SpaceStoneText;

        [Header("Planets")]

        [SerializeField]
        private GameObject BuyRedPlanetButton;

        private int _valueOfSpaceStone;

        private void Awake()
        {
            SaveManager.LoadSave();

            _valueOfSpaceStone = SaveManager.Save.SpaceStone;
            SpaceStoneText.text = _valueOfSpaceStone.ToString();
            BuyRedPlanetButton.SetActive(!SaveManager.Save.IsRedPlanetActive);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _valueOfSpaceStone += 500;
                SpaceStoneText.text = _valueOfSpaceStone.ToString();
            }
        }

        public void ChangeSpaceStoneValue(int value)
        {
            _valueOfSpaceStone += value;
            SpaceStoneText.text = _valueOfSpaceStone.ToString();
            SaveManager.Save.SpaceStone = _valueOfSpaceStone;
            SaveManager.SaveSave();
        }

        public int GetSpaceStoneValue()
        {
            return _valueOfSpaceStone;
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void StartPlay(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}
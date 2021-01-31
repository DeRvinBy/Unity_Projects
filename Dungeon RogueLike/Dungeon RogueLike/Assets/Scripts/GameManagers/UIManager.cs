using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Items;

namespace Scripts.GameManagers
{
    public class UIManager : MonoBehaviour, IManager
    {
        public StatusOfManager status { get; private set; }

        [SerializeField] private Transform UITransform;

        [Header("Floor")]
        [SerializeField] private Transform FloorPanel;
        [SerializeField] private Text FloorText;
        [SerializeField] private float FloorDelay;

        [Header("Weapon")]
        [SerializeField] private Transform GetWeaponPanel;
        [SerializeField] private Image GetWeaponImage;

        [Header("Stats")]
        [SerializeField] private Text GoldText;
        [SerializeField] private Transform HealthContainer;
        [SerializeField] private Transform EnergyContainer;
        [SerializeField] private Sprite[] healthSprite;
        [SerializeField] private Sprite[] energySprite;

        [Header("Fight")]
        [SerializeField] private Transform FightPanel;

        [Header("PopUpText")]
        [SerializeField] private Text PopUpText;

        [Header("GameOver")]
        [SerializeField] private Transform GameOverPanel;
        [SerializeField] private Text GoldTextGameOver;
        [SerializeField] private Text FloorTextGameOver;

        private WeaponData weapon;
        private Animator animatorPopUp;

        public void StartManager()
        {
            animatorPopUp = PopUpText.GetComponent<Animator>();
            status = StatusOfManager.Started;
        }

        #region Floor
        public void ChangeFloor(int value)
        {
            FloorText.text = "Этаж: " + value.ToString();
        }

        public void ActiveFloorPanel()
        {
            FloorPanel.gameObject.SetActive(true);
            Managers.Game.IsPlayerTurn = false;
            Managers.Game.IsPause = true;
            StartCoroutine(WaitFloorDelay());
        }

        IEnumerator WaitFloorDelay()
        {
            yield return new WaitForSeconds(FloorDelay);
            FloorPanel.gameObject.SetActive(false);
            Managers.Game.IsPlayerTurn = true;
            Managers.Game.IsPause = false;
        }
        #endregion

        #region Weapon

        public void GetWeapon(WeaponData weapon)
        {
            Managers.Game.IsPause = true;
            this.weapon = weapon;
            GetWeaponImage.sprite = weapon.sprite;
            GetWeaponPanel.gameObject.SetActive(true);
        }

        public void AcceptGetWeapon()
        {
            Managers.Fight.SetWeapon(weapon);
            Managers.Game.IsPause = false;
            GetWeaponPanel.gameObject.SetActive(false);
        }

        public void CancelGetWeapon()
        {
            Managers.Game.IsPause = false;
            GetWeaponPanel.gameObject.SetActive(false);
        }

        #endregion Weapon

        #region Header
        public void ChangeHealth(int value)
        {
            DisplaySprites(value, HealthContainer, healthSprite);
        }

        public void ChangeEnergy(int value)
        {
            DisplaySprites(value, EnergyContainer, energySprite);
        }

        private void DisplaySprites(int value, Transform container, Sprite[] sprites)
        {
            Image[] icons = container.GetComponentsInChildren<Image>();

            int count = value / 10;

            for (int i = 0; i < count; i++)
                icons[i].sprite = sprites[0];

            if (Mathf.RoundToInt(value / 10f) - count == 1)
            {
                icons[count].sprite = sprites[1];
                count++;
            }

            for (int i = count; i < icons.Length; i++)
                icons[i].sprite = sprites[2];
        }

        public void ChangeGold(int value)
        {
            GoldText.text = value.ToString();
        }
        #endregion

        #region Fight

        public void SetActiveFight(bool isActive)
        {
            FightPanel.gameObject.SetActive(isActive);
        }

        #endregion

        #region PopUp
        public void DisplayPopUpText(string text)
        {
            PopUpText.text = text;
            animatorPopUp.SetTrigger("Play");
        }
        #endregion

        #region GameOver
        public void ActiveGameOverPanel(int gold, int floor)
        {
            for (int i = 0; i < UITransform.childCount; i++)
            {
                UITransform.GetChild(i).gameObject.SetActive(false);
            }
            FloorTextGameOver.text = floor.ToString();
            GoldTextGameOver.text = gold.ToString();
            GameOverPanel.gameObject.SetActive(true);
        }
        #endregion
    }
}
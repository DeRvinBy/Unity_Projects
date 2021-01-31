using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Creature;
using Scripts.Items;

namespace Scripts.GameManagers
{
    public class FightManager : MonoBehaviour, IManager
    {
        public StatusOfManager status { get; private set; }

        [SerializeField] private float EndFightDelay;
        [SerializeField] private int[] ChanceInFight;

        public EnemyMove Enemy;
        public Weapon currentWeapon;

        private PlayerMove Player;

        private void OnValidate()
        {
            if (ChanceInFight.Length == 0) return;

            int sumOfChance = 0;
            for (int i = 0; i < ChanceInFight.Length; i++)
            {
                sumOfChance += ChanceInFight[i];
            }

            if (sumOfChance != 100)
            {
                ChanceInFight[ChanceInFight.Length - 1] += 100 - sumOfChance;
            }
        }

        public void StartManager()
        {
            status = StatusOfManager.Started;
            Player = PlayerMove.instance;
        }

        #region Weapon

        public void SetWeapon(WeaponData weaponData)
        {
            currentWeapon = new Weapon(weaponData.chanceToKill, weaponData.strength);
            Player.SetWeapon(weaponData.sprite);
        }

        public void ChangeWeaponStrenght()
        {
            if (currentWeapon == null) return;

            currentWeapon.strength--;

            if (currentWeapon.strength <= 0)
            {
                Managers.UI.DisplayPopUpText("Оружие сломано.");
                currentWeapon = null;
            }
        }

        #endregion

        #region Fight

        public void StartFight()
        {
            if (Player.transform.position.x < Enemy.transform.position.x)
            {
                Player.transform.rotation = Quaternion.Euler(0, 0, 0);
                Enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (Player.transform.position.x > Enemy.transform.position.x)
            {
                Player.transform.rotation = Quaternion.Euler(0, 180, 0);
                Enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            Managers.Game.IsPlayerTurn = false;
            Managers.Game.IsPause = true;
            Managers.UI.SetActiveFight(true);
        }

        public void RunFromEnemy()
        {
            switch (RandomChance.RandomChoose(ChanceInFight))
            {
                case 0:
                    Enemy.Attack();
                    Managers.UI.DisplayPopUpText("Побег с уроном");
                    StartCoroutine(EndFight());
                    break;
                case 1:
                    Managers.UI.DisplayPopUpText("Побег без урона");
                    StartCoroutine(EndFight());
                    break;
                case 2:
                    Enemy.Attack();
                    Managers.UI.DisplayPopUpText("Побег провален");
                    break;
            }
        }

        public void AttackEnemy()
        {
            if (currentWeapon == null)
            {
                Managers.UI.DisplayPopUpText("Нет оружия");
                return;
            }

            switch (RandomChance.RandomChoose(new int[2] { currentWeapon.chanceToKill, 100 - currentWeapon.chanceToKill }))
            {
                case 0:
                    Player.Attack();
                    Enemy.Damage();
                    StartCoroutine(EndFight());
                    Managers.UI.DisplayPopUpText("Враг убит");
                    break;
                case 1:
                    Player.Attack();
                    StartCoroutine(EndFight());
                    Managers.UI.DisplayPopUpText("Промах");
                    break;
            }
        }

        IEnumerator EndFight()
        {
            Managers.UI.SetActiveFight(false);
            yield return new WaitForSeconds(EndFightDelay);
            Managers.Game.IsPause = false;
            Managers.Game.IsPlayerTurn = true;
        }

        #endregion
    }
}
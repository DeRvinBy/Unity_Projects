using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Creature;
using Scripts.GameManagers;

namespace Scripts.Items
{
    public class Chest : MonoBehaviour, IObjectAction
    {
        [Header("Chest settings")]
        [SerializeField] private int ChestDamage = 5;
        [SerializeField] private int ChestGold = 10;
        [SerializeField] private int[] ChanceOfTypeChest;
        [SerializeField] private int[] ChanceOfTypeWeapon;
        [SerializeField] private List<WeaponData> weapons;

        private Animator animator;
        private TypeOfChest typeOfChest;
        private WeaponData currentWeapon;
        private bool isOpen = false;

        private void OnValidate()
        {
            if (ChanceOfTypeChest.Length == 0) return;

            int sumOfChance = 0;
            for (int i = 0; i < ChanceOfTypeChest.Length; i++)
            {
                sumOfChance += ChanceOfTypeChest[i];
            }

            if(sumOfChance != 100)
            {
                ChanceOfTypeChest[ChanceOfTypeChest.Length - 1] += 100 - sumOfChance;
            }

            if (ChanceOfTypeWeapon.Length == 0) return;

            sumOfChance = 0;
            for (int i = 0; i < ChanceOfTypeWeapon.Length; i++)
            {
                sumOfChance += ChanceOfTypeWeapon[i];
            }

            if (sumOfChance != 100)
            {
                ChanceOfTypeWeapon[ChanceOfTypeWeapon.Length - 1] += 100 - sumOfChance;
            }

        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            animator.SetInteger("TypeOfChest", 0);
            typeOfChest = SetTypeOfChest();
            isOpen = false;
        }

        private TypeOfChest SetTypeOfChest()
        {
            switch (RandomChance.RandomChoose(ChanceOfTypeChest))
            {
                case 0:
                    currentWeapon = SetTypeOfWeapon();
                    return TypeOfChest.Weapon;
                case 1:
                    return TypeOfChest.Enemy;
                default:
                    return TypeOfChest.Gold;
            }
        }

        private WeaponData SetTypeOfWeapon()
        {
            switch (RandomChance.RandomChoose(ChanceOfTypeWeapon))
            {
                case 0:
                    return weapons[0];
                case 1:
                    return weapons[1];
                default:
                    return weapons[2];
            }
        }

        public void Action()
        {
            if (isOpen) return;

            switch(typeOfChest)
            {
                case TypeOfChest.Weapon:
                    Managers.UI.GetWeapon(currentWeapon);
                    break;
                case TypeOfChest.Gold:
                    Managers.Game.ChangeGold(ChestGold);
                    break;
                case TypeOfChest.Enemy:
                    PlayerMove.instance.Damage(ChestDamage);
                    break;
                default:
                    typeOfChest = TypeOfChest.Weapon;
                    break;
            }

            if(typeOfChest != TypeOfChest.Enemy)
                isOpen = true;

            animator.SetInteger("TypeOfChest", (int)typeOfChest);
        }
    }

    public enum TypeOfChest
    {
        Weapon = 1,
        Gold,
        Enemy
    }
}
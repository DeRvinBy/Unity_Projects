using UnityEngine;
using Scripts.GameManagers;
using Scripts.Creature;

namespace Scripts.Items
{
    public class Bank : MonoBehaviour
    {
        [Header("Value Of Bank")]
        [SerializeField] private int littleEnergyValue = 20;
        [SerializeField] private int bigEnergyValue = 50;
        [SerializeField] private int littleHealthValue = 20;
        [SerializeField] private int bigHealthValue = 50;

        [Header("Sprite Of Bank")]
        [SerializeField] private Sprite littleEnergySprite;
        [SerializeField] private Sprite bigEnergySprite;
        [SerializeField] private Sprite littleHealthSprite;
        [SerializeField] private Sprite bigHealthSprite;

        [Header("Chance Of Bank")]
        [SerializeField] private int[] ChanceOfSizeBank;

        private Animator animator;
        private TypeOfBank typeOfBank;
        private Sprite currentSprite;

        private void OnValidate()
        {
            if (ChanceOfSizeBank.Length == 0) return;

            int sumOfChance = 0;
            for (int i = 0; i < ChanceOfSizeBank.Length; i++)
            {
                sumOfChance += ChanceOfSizeBank[i];
            }

            if (sumOfChance != 100)
            {
                ChanceOfSizeBank[ChanceOfSizeBank.Length - 1] += 100 - sumOfChance;
            }
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            typeOfBank = SetupTypeOfBank();
            animator.SetInteger("TypeOfBank", (int)typeOfBank);
            InvokeRepeating("PlayAnimation", Random.Range(2f, 4f), Random.Range(4f, 8f));
        }

        private TypeOfBank SetupTypeOfBank()
        {
            int random = Random.Range(0, 2);
            switch (RandomChance.RandomChoose(ChanceOfSizeBank))
            {
                case 0:
                    if (random == 0)
                        return TypeOfBank.LittleEnergy;
                    else
                        return TypeOfBank.LittleHealth;
                default:
                    if (random == 0)
                        return TypeOfBank.BigEnergy;
                    else
                        return TypeOfBank.BigHealth;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                switch (typeOfBank)
                {
                    case TypeOfBank.LittleEnergy:
                        Managers.Game.ChangeEnergy(littleEnergyValue);
                        currentSprite = littleEnergySprite;
                        break;
                    case TypeOfBank.BigEnergy:
                        Managers.Game.ChangeEnergy(bigEnergyValue);
                        currentSprite = bigEnergySprite;
                        break;
                    case TypeOfBank.LittleHealth:
                        Managers.Game.ChangeHealth(littleHealthValue);
                        currentSprite = littleHealthSprite;
                        break;
                    case TypeOfBank.BigHealth:
                        Managers.Game.ChangeHealth(bigHealthValue);
                        currentSprite = bigHealthSprite;
                        break;
                }

                PlayerMove.instance.GetItem(currentSprite);
                gameObject.SetActive(false);
            }
        }

        private void PlayAnimation()
        {
            animator.SetTrigger("Reload");
        }
    }

    public enum TypeOfBank
    {
        LittleEnergy = 1,
        BigEnergy,
        LittleHealth,
        BigHealth
    }
}
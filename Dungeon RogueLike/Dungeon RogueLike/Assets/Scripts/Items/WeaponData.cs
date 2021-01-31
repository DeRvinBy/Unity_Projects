using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(menuName = "Weapons/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [Range(0,100)][SerializeField] private int _chanceToKill;
        [Range(0,20)][SerializeField] private int _strength;

        public Sprite sprite => _sprite;
        public int chanceToKill => _chanceToKill;
        public int strength => _strength;
    }
}

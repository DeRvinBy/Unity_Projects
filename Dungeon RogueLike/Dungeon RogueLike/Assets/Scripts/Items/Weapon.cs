using UnityEngine;

namespace Scripts.Items
{
    public class Weapon
    {
        public int chanceToKill;
        public int strength;
        public Weapon(int chance, int strength)
        {
            this.chanceToKill = chance;
            this.strength = strength;
        }
    }
}

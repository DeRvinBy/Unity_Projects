using UnityEngine;

namespace Scripts.Game.EnemyObjects
{
    public class LazerEnemyShooting : EnemyShooting
    {
        [Header("LazerComponent")]

        [SerializeField]
        [Tooltip("Prehab of lazer")]
        private GameObject Lazer;

        public override bool Shoot()
        {
            if(!Lazer.activeInHierarchy)
                Lazer.SetActive(true);

            return true;
        }
    }
}
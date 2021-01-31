using System.Collections;
using UnityEngine;
using Scripts.Game.GameObjects;

namespace Scripts.Game.SpawnFactories
{
    public abstract class SpawnFactory : MonoBehaviour
    {
        [Header("Time options of spawn")]

        [SerializeField]
        [Tooltip("Time to start spawn object")]
        protected float StartSpawnTime = 0f;

        [SerializeField]
        [Tooltip("Time to start spawn object")]
        protected float ReloadSpawnTime = 1f;

        [SerializeField]
        [Tooltip("The value that changes ReloadSpawnTime")]
        private float TimeChangeValue = 1f;

        [Header("Positions options of spawn")]

        [SerializeField]
        [Tooltip("Height of spawn point")]
        protected float HeightSpawn = 10f;

        private void Start()
        {
            GameManager._instance.AddListener(EventChangeReload);
        }

        protected virtual void EventChangeReload()
        {
            ReloadSpawnTime -= TimeChangeValue;
        }

        public IEnumerator SpawnCorountine()
        {
            return Spawn();
        }

        protected abstract IEnumerator Spawn();
    }
}

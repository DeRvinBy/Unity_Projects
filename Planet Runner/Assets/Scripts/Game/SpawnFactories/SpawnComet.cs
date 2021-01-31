using System.Collections;
using UnityEngine;
using Scripts.Pool;

namespace Scripts.Game.SpawnFactories
{
    public class SpawnComet : SpawnFactory
    {
        [Header("Prehabs")]

        [SerializeField]
        [Tooltip("Prehab of object to be spawn")]
        private GameObject PrehabToSpawn;

        protected override IEnumerator Spawn()
        {
            yield return new WaitForSeconds(StartSpawnTime);

            while (true)
            {
                Vector3 spawnPoint = new Vector3(0, HeightSpawn, 0);
                PoolManager.GetFromPool(PrehabToSpawn, spawnPoint, Quaternion.identity, PoolContainer.instance.transform);
                yield return new WaitForSeconds(ReloadSpawnTime);
            }
        }
    }
}
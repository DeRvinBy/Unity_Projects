using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Pool;

namespace Scripts.Game.SpawnFactories
{
    public class SpawnEnemy : SpawnFactory
    {
        [Header("Prehabs")]

        [SerializeField]
        [Tooltip("List of enemys to spawn")]
        private List<GameObject> Prehabs = new List<GameObject>();

        protected override IEnumerator Spawn()
        {
            yield return new WaitForSeconds(StartSpawnTime);

            while (true)
            {
                GameObject prehab = Prehabs[Random.Range(0, Prehabs.Count)];
                Vector3 spawnPoint = new Vector3(0, HeightSpawn, 0);

                GameObject gameObject = PoolManager.GetFromPool(prehab, spawnPoint, Quaternion.identity, PoolContainer.instance.transform);

                yield return new WaitWhile(() => gameObject.activeInHierarchy); 

                yield return new WaitForSeconds(ReloadSpawnTime);
            }
        }
    }
}
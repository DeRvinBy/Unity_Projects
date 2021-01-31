using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Pool
{
    public class PoolContainer : MonoBehaviour
    {
        public static PoolContainer instance = null;

        [SerializeField] private List<PoolObjects> PoolObjects;

        public void Awake()
        {
            if (instance != null)
                Destroy(this);
            else
                instance = this;

            Initialize();
        }

        private void Initialize()
        {
            foreach(PoolObjects pool in PoolObjects)
            {
                string poolName = pool.Initialize(transform);
                PoolManager.Initialize(poolName, pool);
            }
        }

        public void AddPool(GameObject prehab)
        {
            PoolObjects pool = new PoolObjects(prehab);
            string poolName = pool.Initialize(transform);
            PoolManager.Initialize(poolName, pool);
            PoolObjects.Add(pool);
        }

        public T[] GetObjectsFromScene<T>()
        {
            return GetComponentsInChildren<T>(false);
        }
    }
}
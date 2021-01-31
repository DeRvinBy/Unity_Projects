using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Pool
{
    public static class PoolManager
    {
        private static Dictionary<string, PoolObjects> _pools = new Dictionary<string, PoolObjects>();

        public static void Initialize(string name, PoolObjects poolObjects)
        {
            if (!_pools.ContainsKey(name))
                _pools[name] = poolObjects;
        }

        public static void Initialize(GameObject prehab)
        {
            if (!_pools.ContainsKey(prehab.name))
                PoolContainer.instance.AddPool(prehab);
        }

        public static GameObject GetFromPool(GameObject prehab, Vector3 positon, Quaternion quaternion, Transform parent)
        {
            GameObject result = InitializeObject(prehab, positon, quaternion);
            result.transform.SetParent(parent);

            return result;
        }

        public static GameObject GetFromPool(GameObject prehab, Vector3 positon, Quaternion quaternion)
        {
            GameObject result = InitializeObject(prehab, positon, quaternion);
            return result;
        }

        private static GameObject InitializeObject(GameObject prehab, Vector3 positon, Quaternion quaternion)
        {
            Initialize(prehab);

            GameObject result = _pools[prehab.name].GetFromPool();

            result.name = prehab.name;
            result.transform.position = positon;
            result.transform.rotation = quaternion;
            result.SetActive(true);

            return result;
        }

        public static void ReturnToPool(GameObject target)
        {
            Initialize(target);

            target.SetActive(false);
            _pools[target.name].ReturnToPool(target);
        }

        public static void OnRestartLevel()
        {
            _pools.Clear();
        }
    }
}
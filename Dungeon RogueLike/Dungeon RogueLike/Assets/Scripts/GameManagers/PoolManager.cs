using System.Collections.Generic;
using UnityEngine;

namespace Scripts.GameManagers
{
    public class PoolManager 
    {
        private static Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

        public static GameObject GetFromPool(GameObject prehab, Vector3 positon, Quaternion quaternion, Transform objectsParent)
        {
            if (!pools.ContainsKey(prehab.name))
                pools[prehab.name] = new List<GameObject>();

            GameObject result;

            if(pools[prehab.name].Count > 0)
            {
                int i = 0;
                while(i < pools[prehab.name].Count)
                {
                    if(!pools[prehab.name][i].activeInHierarchy)
                    {
                        result = pools[prehab.name][i];
                        result.transform.position = positon;
                        result.transform.rotation = quaternion;
                        result.SetActive(true);
                        return result;
                    }

                    i++;
                }
            }

            result = GameObject.Instantiate(prehab, positon, quaternion, objectsParent);
            result.name = prehab.name;
            pools[prehab.name].Add(result);
            result.SetActive(true);

            return result;
        }

        public static void ReturnToPool(GameObject target)
        {
            target.SetActive(false);
        }

        public static void OnRestartLevel()
        {
            pools.Clear();
        }
    }
}
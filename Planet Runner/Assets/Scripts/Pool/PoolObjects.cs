using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.Pool
{
    [Serializable]
    public class PoolObjects
    {
        [SerializeField] private GameObject Prehab;
        [SerializeField] private int CountOfOjects;

        private Stack<GameObject> _prehabs;
        private Transform _parent;

        public PoolObjects(GameObject prehab, int countOfObjects = 1)
        {
            this.Prehab = prehab;
            this.CountOfOjects = countOfObjects;
        }

        public string Initialize(Transform parent)
        {
            _prehabs = new Stack<GameObject>();
            _parent = parent;

            for (int i = 0; i < CountOfOjects; i++)
            {
                _prehabs.Push(InstantiateObject());
            }

            return Prehab.name;
        }

        public void ReturnToPool(GameObject gameObject)
        {
            gameObject.transform.SetParent(_parent);
            _prehabs.Push(gameObject);
        }

        public GameObject GetFromPool()
        {
            if (_prehabs.Count == 0)
                return InstantiateObject();
            else
                return _prehabs.Pop();
        }

        private GameObject InstantiateObject()
        {
            GameObject gameObject = GameObject.Instantiate(Prehab, _parent);
            gameObject.SetActive(false);
            gameObject.name = Prehab.name;
            return gameObject;
        }
    }
}
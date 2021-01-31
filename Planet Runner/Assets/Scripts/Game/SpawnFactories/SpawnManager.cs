using UnityEngine;

namespace Scripts.Game.SpawnFactories
{
    public class SpawnManager : MonoBehaviour
    {
        private SpawnFactory[] Factories;

        private void Awake()
        {
            Factories = GetComponents<SpawnFactory>();

            foreach (SpawnFactory factory in Factories)
                StartCoroutine(factory.SpawnCorountine());
        }
    }
}
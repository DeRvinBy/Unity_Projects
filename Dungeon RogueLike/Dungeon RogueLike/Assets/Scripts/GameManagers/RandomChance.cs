using Random = UnityEngine.Random;

namespace Scripts.GameManagers
{
    public class RandomChance
    {
        static public int RandomChoose(float[] probs)
        {
            float total = 0;

            foreach (float elem in probs)
                total += elem;

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                    return i;
                else
                    randomPoint -= probs[i];
            }

            return probs.Length - 1;
        }

        static public int RandomChoose(int[] probs)
        {
            int total = 0;

            foreach (int elem in probs)
                total += elem;

            int randomPoint = (int)(Random.value * total);

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                    return i;
                else
                    randomPoint -= probs[i];
            }

            return probs.Length - 1;
        }
    }
}

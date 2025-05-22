using System.Collections.Generic;
using UnityEngine;

public class AlchemyManager : MonoBehaviour
{
    public static AlchemyManager Instance;

    [System.Serializable]
    public class Combination
    {
        public GameObject inputA;
        public GameObject inputB;
        public GameObject result;
    }

    public List<Combination> combinations;

    private void Awake()
    {
        Instance = this;
    }

    public void TryCombine(AlchemyItem a, AlchemyItem b)
    {
        GameObject prefabA = a.itemPrefab;
        GameObject prefabB = b.itemPrefab;

        foreach (var combo in combinations)
        {
            bool match = (combo.inputA == prefabA && combo.inputB == prefabB) ||
                         (combo.inputA == prefabB && combo.inputB == prefabA);

            if (match)
            {
                Vector3 spawnPos = (a.transform.position + b.transform.position) / 2f;
                Quaternion spawnRot = Quaternion.Lerp(a.transform.rotation, b.transform.rotation, 0.5f);
                Instantiate(combo.result, spawnPos, spawnRot);

                Destroy(a.gameObject);
                Destroy(b.gameObject);
                return;
            }
        }
    }
}

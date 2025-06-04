using System.Collections.Generic;
using UnityEngine;

public class AlchemyManager : MonoBehaviour
{
    public static AlchemyManager Instance;

    [System.Serializable]
    public class Combination
    {
        public AlchemyItemDefinition inputA;
        public AlchemyItemDefinition inputB;
        public GameObject result;
    }

    public List<Combination> combinations;

    [Header("Audio")]
    public AudioClip fusionSFX;

    private void Awake()
    {
        Instance = this;
    }

    public void TryCombine(AlchemyItem a, AlchemyItem b)
    {
        Debug.Log($"Trying to combine {a.definition.itemName} and {b.definition.itemName}");

        foreach (var combo in combinations)
        {
            Debug.Log($"Checking combo: {combo.inputA.itemName} + {combo.inputB.itemName}");

            if ((combo.inputA == a.definition && combo.inputB == b.definition) ||
                (combo.inputA == b.definition && combo.inputB == a.definition))
            {
                Debug.Log("Combination match found!");

                Vector3 spawnPos = (a.transform.position + b.transform.position) / 2;

                if (fusionSFX != null)
                    AudioSource.PlayClipAtPoint(fusionSFX, spawnPos);

                Destroy(a.gameObject);
                Destroy(b.gameObject);
                Instantiate(combo.result, spawnPos, Quaternion.identity);
                return;
            }
        }

        Debug.Log("No matching combination found.");
    }
}

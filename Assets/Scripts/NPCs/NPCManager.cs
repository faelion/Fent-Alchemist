using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject[] npcPrefabs;
    private List<GameObject> npcList = new();

    void Start()
    {
        PatrolPoints[] patrols = FindObjectsByType<PatrolPoints>(FindObjectsSortMode.None);

        foreach (PatrolPoints patrol in patrols)
        { 
            GameObject prefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
            GameObject npc = Instantiate(prefab, new Vector3(12,2,3), Quaternion.identity);
            npc.GetComponent<NPCController>().SetPatrolPoints(patrol.points);
            npcList.Add(npc);
        }
    }
}

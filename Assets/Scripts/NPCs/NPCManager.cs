using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;
    [SerializeField] private GameObject[] npcPrefabs;
    private List<GameObject> npcList = new();
    public List<NPCOrder> activeOrders = new();

    private void Awake()
    {
        Instance = this;
    }

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

    public bool IsItemRequested(AlchemyItemDefinition item)
    {
        return activeOrders.Exists(order => !order.completed && order.requestedItem == item && order.active);
    }

    public NPCOrder GetOrderForItem(AlchemyItemDefinition item)
    {
        return activeOrders.Find(order => !order.completed && order.requestedItem == item);
    }
    public void AddOrder(NPCOrder order)
    {
        activeOrders.Add(order);
    }

    public NPCOrder AssignOrderToNPC(NPCCostumer npc)
    {
        // Por ejemplo: asignamos la primera misión sin completar
        var order = activeOrders.Find(o => !o.completed && o.assignedTo == null);

        if (order != null)
        {
            order.assignedTo = npc;
        }

        return order;
    }

    public void RemoveNPC(NPCController npc)
    {
        if (npcList.Contains(npc.gameObject))
        {
            npcList.Remove(npc.gameObject);
            Debug.Log($"NPC {npc.name} eliminado de la lista.");
            Destroy(npc.gameObject);
        }
        else
        {
            Debug.LogWarning($"El NPC {npc.name} no se encuentra en la lista.");
        }
    }

}


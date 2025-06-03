using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;
    [SerializeField] private GameObject[] npcPrefabs;
    private List<GameObject> npcList = new();
    public List<NPCOrder> activeOrders = new();
    public GameObject UICanvas;
    public GameObject UIOrderPrefab;
    public List<GameObject> UIOrders = new();
    private int currentOrderIndex = 0;
    public float timeBetweenOrders = 5f;
    private float currentTime = 0f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateNPC();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeBetweenOrders)
        {
            currentTime = 0f;
            if(currentOrderIndex < activeOrders.Count)
            CreateNPC();
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
            currentOrderIndex++;
        }

        return order;
    }

    public void RemoveUIOrder(NPCOrder order)
    {
        if (UIOrders == null || UIOrders.Count == 0)
        {
            Debug.LogWarning("No hay órdenes en la lista de UIOrders para eliminar.");
            return;
        }
        else
        {
            foreach (GameObject uiOrder in UIOrders)
            {
                if (uiOrder.GetComponentInChildren<UIOrders>().currentOrder == order)
                {
                    UIOrders.Remove(uiOrder);
                    Debug.Log($"Orden {order.requestedItem.itemName} eliminada de la UI.");
                    Destroy(uiOrder);
                    return;
                }
            }
        }
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

    private void CreateNPC()
    {
        PatrolPoints[] patrols = FindObjectsByType<PatrolPoints>(FindObjectsSortMode.None);
        GameObject prefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
        GameObject npc = Instantiate(prefab, new Vector3(12, 2, 3), Quaternion.identity);
        npc.GetComponent<NPCController>().SetPatrolPoints(patrols[0].points);
        npcList.Add(npc);
    }

}


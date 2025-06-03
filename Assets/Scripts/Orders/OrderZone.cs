using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        NPCCostumer npc = other.GetComponent<NPCCostumer>();
        if (npc == null || npc.currentOrder == null || npc.currentOrder.active)
            return;

        npc.AddOrder();

        GameObject uiOrderPrefab = Instantiate(NPCManager.Instance.UIOrderPrefab, NPCManager.Instance.UICanvas.transform);
        uiOrderPrefab.GetComponentInChildren<UIOrders>().currentOrder = npc.currentOrder;
        uiOrderPrefab.GetComponentInChildren<UIOrders>().UpdateOrderText();
        NPCManager.Instance.UIOrders.Add(uiOrderPrefab);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        NPCCostumer npc = other.GetComponent<NPCCostumer>();
        if (npc == null || npc.currentOrder == null)
            return;
        npc.AddOrder();
    }
}

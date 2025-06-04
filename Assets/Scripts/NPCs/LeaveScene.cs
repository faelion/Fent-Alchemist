using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        NPCController npc = other.GetComponent<NPCController>();
        if (npc == null)
        {
            Debug.Log($"El objeto {other.gameObject.name} no es un NPCCostumer.");
            return;
        }

        NPCManager.Instance.RemoveNPC(npc);
    }
}

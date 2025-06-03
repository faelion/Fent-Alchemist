using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCostumer : NPCController
{
    // Start is called before the first frame update
    private int currentPointIndex = 0;
    public float speed = 3f;
    public NPCOrder currentOrder = null;

    void Start()
    {

    }
    public override void DoBehavior()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        agent.SetDestination(targetPoint.position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    public void AddOrder()
    {
        currentOrder = NPCManager.Instance.AssignOrderToNPC(this);

        if (currentOrder != null)
        {
            Debug.Log($"{name} ha recibido la misi�n de pedir {currentOrder.requestedItem.itemName}");
            currentOrder.active = true;
        }
        else
        {
            Debug.LogWarning($"{name} no ha recibido ninguna misi�n.");
        }
    }

}

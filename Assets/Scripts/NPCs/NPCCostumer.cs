using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCostumer : NPCController
{
    // Start is called before the first frame update
    private int currentPointIndex = 0;
    public float speed = 3f;
    public NPCOrder currentOrder = null;
    private float maxTimeToWait = 60f;
    private float currentWaitTime = 0f;
    bool waitingForOrder = false;

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

        if(waitingForOrder)
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime >= maxTimeToWait)
            {
                EndOrder();
            }
        }
    }

    public void AddOrder()
    {
        currentOrder = NPCManager.Instance.AssignOrderToNPC(this);

        if (currentOrder != null)
        {
            Debug.Log($"{name} ha recibido la misión de pedir {currentOrder.requestedItem.itemName}");
            currentOrder.active = true;
            waitingForOrder = true;
            maxTimeToWait = currentOrder.timeToComplete;
        }
        else
        {
            Debug.LogWarning($"{name} no ha recibido ninguna misión.");
        }
    }

    public void EndOrder()
    {
        if (currentOrder != null && !currentOrder.completed)
        {
            NPCManager.Instance.playerData.Currency -= currentOrder.rewardMoney;
            NPCManager.Instance.RemoveUIOrder(currentOrder);
        }
        LeaveScene();
    }

}

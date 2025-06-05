using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOrders : MonoBehaviour
{
    public TextMeshProUGUI orderText;
    public NPCOrder currentOrder;

    private void Start()
    {
       
    }

    public void UpdateOrderText()
    {
        string text = "Active Order:\n";
        if (currentOrder != null)
        {
            text += $"{currentOrder.requestedItem.itemName} - Assigned to: {currentOrder.assignedTo?.name ?? "None"}\n";
            text += $"Reward: {currentOrder.rewardMoney} coins\n";
            currentOrder.timeToComplete -= Time.deltaTime;
            text += $"Time to complete: {(int)currentOrder.timeToComplete} seconds\n";
        }

        orderText.text = text;
    }
}

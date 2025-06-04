using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCOrder
{
    public AlchemyItemDefinition requestedItem;
    public int rewardMoney;
    public bool active;
    public bool completed;
    public NPCCostumer assignedTo;
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICurrency : MonoBehaviour
{
    public TextMeshProUGUI orderText;

    private void Start()
    {

    }

    public void Update()
    {
        if (NPCManager.Instance != null && NPCManager.Instance.playerData != null)
        {
            orderText.text = $"Coins: {NPCManager.Instance.playerData.Currency}";
        }
        else
        {
            orderText.text = "Coins: 0";
        }
    }
}

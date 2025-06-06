using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShadowText : MonoBehaviour
{
    public TMP_Text originalTextComponent;
    public TMP_Text shadowTextComponent;

    private void Update()
    {
        if(originalTextComponent == null || shadowTextComponent == null)
        {
            Debug.LogWarning("Original or Shadow Text Component is not assigned.");
            return;
        }

        if (originalTextComponent.text != shadowTextComponent.text)
        {
            shadowTextComponent.text = originalTextComponent.text;
        }
    }
}

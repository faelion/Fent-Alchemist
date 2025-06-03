using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualVariant : MonoBehaviour
{
    public List<GameObject> variants = new List<GameObject>();
    public float yOffset = 0.0f;

    private void Start()
    {
        if (variants.Count == 0)
        {
            Debug.LogWarning("No variants assigned to VisualVariant.");
            return;
        }

        GameObject.Instantiate(variants[Random.Range(0, variants.Count)], transform.position + new Vector3(0f,yOffset,0f), transform.rotation, transform);
    }
}

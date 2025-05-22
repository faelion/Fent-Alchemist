using UnityEngine;

public class AlchemyItem : MonoBehaviour
{
    public AlchemyItemDefinition definition;

    void Start()
    {
        if (definition == null)
        {
            Debug.LogWarning($"{gameObject.name} has no definition assigned!");
        }
        else
        {
            Debug.Log($"{gameObject.name} has definition: {definition.itemName}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{name} collided with {other.name}");

        AlchemyItem otherItem = other.GetComponent<AlchemyItem>();
        if (otherItem != null && otherItem != this)
        {
            Debug.Log("Both objects are valid AlchemyItems. Attempting to combine...");
            AlchemyManager.Instance.TryCombine(this, otherItem);
        }
    }
}
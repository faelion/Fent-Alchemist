using UnityEngine;

public class AlchemyItem : MonoBehaviour
{
    public GameObject itemPrefab;

    private void OnTriggerEnter(Collider other)
    {
        AlchemyItem otherItem = other.GetComponent<AlchemyItem>();
        if (otherItem != null && otherItem != this)
        {
            AlchemyManager.Instance.TryCombine(this, otherItem);
        }
    }
}

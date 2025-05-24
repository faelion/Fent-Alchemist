using UnityEngine;
using System.Collections.Generic;

public class ToolHitTransformer : MonoBehaviour
{
    [Tooltip("Configure each AlchemyIngredient, its result, and how many hits are needed.")]
    public List<HitCombination> hitCombinations;

    private void OnTriggerEnter(Collider other)
    {
        AlchemyItem item = other.GetComponent<AlchemyItem>();
        if (item == null) return;

        HitCombination combo = hitCombinations
            .Find(c => c.targetIngredient == item.definition);
        if (combo == null) return;

        var tracker = item.GetComponent<HitTracker>();
        if (tracker == null)
        {
            tracker = item.gameObject.AddComponent<HitTracker>();
            tracker.Initialize(combo.hitsRequired, combo.result);
        }

        tracker.RegisterHit();
    }
}

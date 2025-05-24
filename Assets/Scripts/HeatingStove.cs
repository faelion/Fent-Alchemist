using UnityEngine;
using System.Collections.Generic;

public class StoveHeatTransformer : MonoBehaviour
{
    [Tooltip("List each AlchemyIngredient, how long to heat it, and its result.")]
    public List<HeatRecipe> heatRecipes;

    private void OnTriggerStay(Collider other)
    {
        // Is it an AlchemyItem?
        AlchemyItem item = other.GetComponent<AlchemyItem>();
        if (item == null) return;

        // Find matching recipe
        HeatRecipe recipe = heatRecipes
            .Find(r => r.targetIngredient == item.definition);
        if (recipe == null) return;

        // Get or add the per-instance tracker
        var tracker = item.GetComponent<HeatTracker>();
        if (tracker == null)
        {
            tracker = item.gameObject.AddComponent<HeatTracker>();
            tracker.Initialize(recipe.timeRequired, recipe.result);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If item leaves the stove before finishing, stop tracking
        var tracker = other.GetComponent<HeatTracker>();
        if (tracker != null)
            Destroy(tracker);
    }
}

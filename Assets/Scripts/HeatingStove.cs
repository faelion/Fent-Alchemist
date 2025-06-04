using UnityEngine;
using System.Collections.Generic;

public class StoveHeatTransformer : MonoBehaviour
{
    [Tooltip("List each AlchemyIngredient, how long to heat it, and its result.")]
    public List<HeatRecipe> heatRecipes;

    [Header("Audio")]
    public AudioClip roastSFX;
    public AudioClip transformSFX;

    private void OnTriggerStay(Collider other)
    {
        AlchemyItem item = other.GetComponent<AlchemyItem>();
        if (item == null) return;

        HeatRecipe recipe = heatRecipes
            .Find(r => r.targetIngredient == item.definition);
        if (recipe == null) return;

        var tracker = item.GetComponent<HeatTracker>();
        if (tracker == null)
        {
            tracker = item.gameObject.AddComponent<HeatTracker>();
            tracker.Initialize(recipe.timeRequired, recipe.result, roastSFX, transformSFX);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var tracker = other.GetComponent<HeatTracker>();
        if (tracker != null)
            Destroy(tracker);
    }
}

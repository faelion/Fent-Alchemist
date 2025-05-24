using UnityEngine;

[System.Serializable]
public class HeatRecipe
{
    public AlchemyItemDefinition targetIngredient;
    public GameObject result;
    public float timeRequired = 3f;
}

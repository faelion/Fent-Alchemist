using UnityEngine;

[System.Serializable]
public class HitCombination
{
    public AlchemyItemDefinition targetIngredient;
    public GameObject result;
    public int hitsRequired = 3;
}
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Create/Alchemy/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public IngredientType ingredientType;
}
public enum IngredientType{Standard, Water}
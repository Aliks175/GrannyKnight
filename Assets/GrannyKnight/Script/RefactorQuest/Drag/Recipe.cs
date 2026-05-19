using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Recipe", menuName = "Create/Alchemy/Recipe")]
public class Recipe : ScriptableObject
{
    public List<Ingredient> ingredients;
    public Ingredient result;
}

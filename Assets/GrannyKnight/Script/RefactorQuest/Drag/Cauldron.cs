using System.Collections.Generic;
using UnityEngine;
public class Cauldron : MonoBehaviour, IngredientIncome
{
    [SerializeField] private List<Recipe> _recipes;
    [SerializeField] private Recipe _winRecipe;
    private List<Ingredient> _ingredients;
    public void Income(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }

    public void CheckRecipe()
    {
        foreach (var recipe in _recipes)
        {
            if (MatchRecipe(recipe))
            {
               if (recipe == _winRecipe)
                {
                    Debug.Log("Win");
                    return;
                }
                _ingredients.Clear();
                _ingredients.Add(recipe.result);
                return;
            }
            Debug.Log("No match");
        }
    }

    bool MatchRecipe(Recipe recipe)
    {
        if (recipe.ingredients.Count != _ingredients.Count)
            return false;

        foreach (var ing in recipe.ingredients)
        {
            if (!_ingredients.Contains(ing))
                return false;
        }

        return true;
    }
}
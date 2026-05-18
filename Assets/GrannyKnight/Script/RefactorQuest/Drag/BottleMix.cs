using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class BottleMix :IngredientObject, IngredientIncome
{
    public List<Ingredient> ingredients = new List<Ingredient>();
    [SerializeField] private Recipe _recipe;
    [Header("Ивенты")]
    public  UnityEvent OnBottleMix;
    public  UnityEvent OnBottleComplete;
    int shakeCount = 0;

    [Inject]
    public void Construct(DragManager quest)
    {
        _recipe = quest.RecipeWater;
    }
    void Awake()
    {
        ingredients.Clear();
    }
    bool movingUp = false;
    public void Income(Ingredient ingredient)
    {
        if (ingredients.Count >= 2 || Ingredient != null) return;
        if (ingredient.ingredientType == IngredientType.Water) ingredients.Add(ingredient); 
        Debug.Log("Добавлено " + ingredient);
    }
    
    void FixedUpdate()
    {
        float current = Input.GetAxis("Mouse Y");

        if(current > 0.3f && !movingUp)
        {
            movingUp = true;
            shakeCount++;
            OnBottleMix?.Invoke();
        }

        if(current < -0.3f)
        {
            movingUp = false;
        }

        if(shakeCount >= 15)
        {
            CompleteShake();
        }
    }
    void CompleteShake()
    {
        shakeCount = 0;
        if (ingredients.Count < 2)
        {
            ingredients.Clear();
            Debug.Log("Не хватает ингредиентов");
            return;
        }
        if (MatchRecipe(_recipe))
            {
                Debug.Log("Совпало " + _recipe.name);
                Ingredient = _recipe.result;
                ingredients.Clear();
                return;
            }
        ingredients.Clear();
        OnBottleComplete?.Invoke();
        Debug.Log("Не совпало ");
    }
    bool MatchRecipe(Recipe recipe)
    {
        if (recipe.ingredients.Count != ingredients.Count)
            return false;

        foreach (var ing in recipe.ingredients)
        {
            if (!ingredients.Contains(ing))
                return false;
        }

        return true;
    }
}

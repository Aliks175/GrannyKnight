using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Pot : MonoBehaviour, IngredientIncome
{
    [SerializeField] private Ingredient _water;
    [SerializeField] private List<Ingredient> _ingredients = new List<Ingredient>();
    private Recipe _recipeWater;
    private Recipe _recipeFinal;
    private DragManager _dragManager;
    [Header("Ивенты")]
    public UnityEvent OnPotAdd;
    public  UnityEvent OnPotEndGood;
    public  UnityEvent OnPotEndBad;

    [Inject]
    public void Construct(DragManager quest)
    {
        _recipeWater = quest.RecipePot;
        _recipeFinal = quest.RecipeFinal;
        _dragManager = quest;
    }
    void Awake()
    {
        _water = null;
        _ingredients.Clear();
    }
    public void Income(Ingredient ingredient)
    {
        if (ingredient == null) return;
        if (ingredient.ingredientType == IngredientType.Water) AddWater(ingredient);
        if (ingredient.ingredientType == IngredientType.Standard) AddFinal(ingredient);
        OnPotAdd?.Invoke();
    }
    void AddWater(Ingredient ingredient)
    {
        _water = ingredient;   
    }
    void AddFinal(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
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
    public void RecipeCheck()
    {
        if (_recipeFinal.ingredients.Count == _ingredients.Count && _water != null)
        {
            if (MatchRecipe(_recipeFinal) && _recipeWater.result == _water)
            {
                _dragManager.StopQuest(QuestEnding.Good);
                Debug.Log("Good ending");
                OnPotEndGood?.Invoke();
            } 
            else
            {
                _dragManager.StopQuest(QuestEnding.Bad);
                Debug.Log("Bad ending");
                OnPotEndBad?.Invoke();
            } 
        }
    }
}

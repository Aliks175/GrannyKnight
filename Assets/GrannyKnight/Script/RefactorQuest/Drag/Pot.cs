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
    private DragQuest _dragManager;
    private int _countBad = 0;
    private int _countNeed = 3;
    [Header("Ивенты")]
    public UnityEvent OnPotAdd;
    public  UnityEvent OnPotEndGood;
    public  UnityEvent OnPotEndBad;

    [Inject]
    public void Construct(DragQuest quest)
    {
        _recipeWater = quest.RecipePot;
        _recipeFinal = quest.RecipeFinal;
        _dragManager = quest;
    }
    void Awake()
    {
        _water = null;
        CleareIng();
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
        if (_water != null) return;
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
        if (MatchRecipe(_recipeFinal) && _recipeWater.result == _water)
        {
            _countBad = 0;
            _dragManager.StopQuest(QuestEnding.Good);
            Debug.Log("Good ending");
            OnPotEndGood?.Invoke();
            
        } 
        else
        {
            _countBad++;
            if (_countBad >= _countNeed) Lose();               
        } 
        
        CleareIng();
    }
    public void CleareIng()
    {
        _ingredients.Clear();
        _water = null;
    }
    private void Lose()
    {
        _countBad = 0;
        _dragManager.StopQuest(QuestEnding.Bad);   
        Debug.Log("Bad ending");
        OnPotEndBad?.Invoke();
        
    }
}

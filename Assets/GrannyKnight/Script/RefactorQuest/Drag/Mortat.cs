using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Mortat : MonoBehaviour, IngredientIncome
{
    private Recipe _recipes;
    private int _clicsNeed = 5;
    [SerializeField] private GameObject _createPrefab;
    [SerializeField] private Transform _positionCreate;
    [SerializeField] private LayerMask _layerTo;
    [Header("Ивенты")]
    public UnityEvent OnMortarClick;
    public  UnityEvent OnMortarIncome;
    public  UnityEvent OnMortarCreate;
    private List<Ingredient> _ingredients = new List<Ingredient>();
    private int _clickCount = 0;    
    private List<GameObject> _createdObjects = new List<GameObject>();
    private GameObject _tempObj;
    private LayerMask _base;
    
    void Awake()
    {
        _base = this.gameObject.layer;
        _ingredients.Clear();
    }
    [Inject]
    public void Construct(DragManager quest)
    {
        _recipes = quest.RecipeMortar;
        _clicsNeed = quest.NumberMortar;
    }
    void OnMouseDown()
    {
        _clickCount++;
        if (_clickCount >= _clicsNeed)
        {
            Debug.Log("Mortat work");
            if (MatchRecipe(_recipes))
            {
                Debug.Log("Совпало " + _recipes.name);
                GameObject temp = Instantiate(_createPrefab, _positionCreate.position, Quaternion.identity);
                temp.GetComponent<IngredientObject>().Ingredient= _recipes.result;
                _ingredients.Clear();
                OnMortarCreate?.Invoke();
                foreach (var obj in _createdObjects)
                {
                    Destroy(obj);
                }
                _clickCount = 0;
                gameObject.layer = (int)Mathf.Log(_layerTo.value, 2);
                return;
            }
            _ingredients.Clear();
            foreach (var obj in _createdObjects)
            {
                Destroy(obj);
            }
            Debug.Log("Не Совпало ");
        }
        OnMortarClick?.Invoke();
    }
    public void Income(Ingredient ingredient)
    {
        if (ingredient == null) return;
        if (_ingredients.Count >= 2) return;
        if (_tempObj == null) return;
        if (ingredient.ingredientType == IngredientType.Water) return;
        _ingredients.Add(ingredient);
        GameObject temp = Instantiate(_tempObj, _positionCreate.position, Quaternion.identity, this.transform);
        _createdObjects.Add(temp);
        temp.GetComponent<Rigidbody>().isKinematic = true;
        //temp.layer = LayerMask.NameToLayer("AlchimiIng");
        temp.transform.localScale*=0.5f;
        _clickCount = 0;
        OnMortarIncome?.Invoke();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IngredientObject>(out var component))
        {
            if (component.Ingredient == null) return;
            if (gameObject.layer != _base) gameObject.layer = _base;
            if (component.Ingredient.ingredientType == IngredientType.Standard)
            {
                _tempObj = other.gameObject;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IngredientObject>(out var component))
        {
            if (component.Ingredient == null) return;
            if (component.Ingredient.ingredientType == IngredientType.Standard)
            {
                _tempObj = null;
            }
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

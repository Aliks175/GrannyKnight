using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class IngredientObject : MonoBehaviour
{
    public Ingredient Ingredient;
    [SerializeField] private bool _isReturn;
    [Header("Ивенты")]
    public UnityEvent OnObjectTake;
    public  UnityEvent OnObjectRelese;
    private Vector3 offset = new Vector3(0,0.5f,0);
    private Vector3 _startPosition;
    private bool isDragging = false;
    private Plane dragPlane;
    private Collider col;
    private IngredientIncome _ingredientIncome;
    private void Awake()
    {
        col = GetComponent<Collider>();
        _startPosition = transform.position;
    }
    public void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        isDragging = true;
        dragPlane = new Plane(-Camera.main.transform.forward,transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        OnObjectTake?.Invoke();
        if (dragPlane.Raycast(ray, out float enter))
        {
            offset = transform.position - ray.GetPoint(enter);
        }
    }
    void OnMouseUp()
    {
        Debug.Log("Mouse Up");
        StopDragging();
    }
    private void Update()
    {
        if (transform.position != _startPosition && _isReturn) transform.position = Vector3.Lerp(transform.position, _startPosition, Time.deltaTime * 10f);
        
        if (!isDragging) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (dragPlane.Raycast(ray, out float enter))
        {
            transform.position =  ray.GetPoint(enter) + offset;
        }
    }

    private void StopDragging()
    {
        isDragging = false;
        col.enabled = true;
        OnObjectRelese?.Invoke();
        if (_ingredientIncome != null) _ingredientIncome.Income(Ingredient);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IngredientIncome>(out var component)) _ingredientIncome = component;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IngredientIncome>(out var component)) _ingredientIncome = null;
    }

}

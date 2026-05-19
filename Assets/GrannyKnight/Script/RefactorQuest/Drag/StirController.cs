using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class StirController : MonoBehaviour
{
    [Header("Центр круга")]
    [SerializeField] private Transform stirCenter;
    [Header("Настройки")]
    [SerializeField]
    private int requiredCircles = 3;
    [SerializeField] private Pot _cauldron;
    [SerializeField] private float minRadius = 100f;
    private Camera cam;
    private bool stirring;
    private float lastAngle;
    private float accumulatedAngle;
    private int completedCircles;
    [Header("Ивенты")]
    public UnityEvent OnStartCircle;
    public UnityEvent OnEndCircle;
    public UnityEvent OnStopCircle;

    [Inject]
    public void Construct(Camera camera)
    {
        cam = camera;
    }

    private void Update()
    {
        if (!stirring)
            return;

        TrackMouseRotation();
    }

    public void StartStirring()
    {
        stirring = true;

        accumulatedAngle = 0;

        completedCircles = 0;

        Vector3 screenCenter = cam.WorldToScreenPoint(stirCenter.position);

        Vector2 dir = (Vector2)Input.mousePosition - (Vector2)screenCenter;

        lastAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        OnStartCircle?.Invoke();
    }

    public void StopStirring()
    {
        stirring = false;
        OnStopCircle?.Invoke();
    }

    private void TrackMouseRotation()
    {
        Vector3 screenCenter = cam.WorldToScreenPoint(stirCenter.position);

        Vector2 dir = (Vector2)Input.mousePosition - (Vector2)screenCenter;

        // Игрок слишком близко к центру
        if (dir.magnitude < minRadius) return;

        float currentAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float delta = Mathf.DeltaAngle(lastAngle, currentAngle);

        accumulatedAngle += delta;

        lastAngle = currentAngle;

        Debug.Log("Angle: " + accumulatedAngle);

        if (Mathf.Abs(accumulatedAngle) >= 360f)
        {
            accumulatedAngle = 0;

            completedCircles++;

            Debug.Log("Круг завершён: " + completedCircles);

            if (completedCircles >= requiredCircles)
            {
                CompleteStirring();
            }
        }
    }

    private void CompleteStirring()
    {
        stirring = false;
        OnEndCircle?.Invoke();
        _cauldron.RecipeCheck();
        Debug.Log("Перемешивание завершено");
    }
    void OnMouseDown()
    {
        StartStirring();
    }
    void OnMouseUp()
    {
        StopStirring();
    }
}
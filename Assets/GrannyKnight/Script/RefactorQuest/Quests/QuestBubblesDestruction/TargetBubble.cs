using UnityEngine;
using Zenject;

public class TargetBubble : MonoBehaviour, IHealtheble, ITarget
{
    public GameObject Body => gameObject;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _body;
    private Collider _collider;
    private ControlTarget _controlTarget;
    private bool _isAlive;

    [Inject]
    public void Construct(ControlTarget controlBubbles)
    {
        _controlTarget = controlBubbles;
        _isAlive = true;
        _controlTarget.OnStartQuest += Start;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _controlTarget.OnStopQuest += OnStopQuest;
    }

    private void OnDisable()
    {
        _controlTarget.OnStartQuest -= Start;
    }

    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        if (!_isAlive) { return; }
        _isAlive = false;
        _controlTarget.AddCountTargetDestruction();
        _collider.enabled = false;
        _body.SetActive(false);
        _particleSystem.Play();
        OnStopQuest();
    }

    private void OnStopQuest()
    {
        _controlTarget.OnStopQuest -= OnStopQuest;
        Destroy(gameObject, 1f);
    }

}
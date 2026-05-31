using UnityEngine;
using Zenject;

public class TargetBubble : MonoBehaviour, IHealtheble, ITarget
{
    public GameObject Body => gameObject;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _body;
    [SerializeField] private SpriteRotate _spriteRotate;
    private Collider _collider;
    private ControlTarget _controlTarget;
    private bool _isAlive;

    [Inject]
    public void Construct(ControlTarget controlBubbles,Transform transform)
    {
        _controlTarget = controlBubbles;
        _isAlive = true;
        _controlTarget.OnStartQuest += Start;
        _spriteRotate.SetTarget(transform);
        _controlTarget.OnStopQuest += OnStopQuest;
    }

    private void OnEnable()
    {
        _collider = GetComponent<Collider>();
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
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
        _controlTarget.AddTarget(this);
        _isAlive = true;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnDisable()
    {
        Destroy(gameObject, 1f);
    }

    public void TakeDamage(float damage)
    {
        if (!_isAlive) { return; }
        _isAlive = false;
        _controlTarget.AddCountTargetDestruction();
        _collider.enabled = false;
        _body.SetActive(false);
        _particleSystem.Play();
    }
}
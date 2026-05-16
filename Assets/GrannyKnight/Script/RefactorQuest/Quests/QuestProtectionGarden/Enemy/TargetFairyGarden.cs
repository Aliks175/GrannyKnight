using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class TargetFairyGarden : MonoBehaviour, IHealtheble
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private DangerZone _dangerZone;
    [Header("SettingsAttack")]
    [SerializeField] private float _timeWaitAttack;
    [Header("SettingsMove")]
    [SerializeField] private Ease _ease;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _duration;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;

    private FairySimpleMove _fairySimpleMove;
    private FairyAnimation _fairyAnimation;
    private FairyAttack _fairyAttack;
    private Animator _animator;
    private bool _isAlife;

    [Inject]
    public virtual void Construct(FairySimpleMove fairySimpleMove, FairyAttack fairyAttack, FairyAnimation fairyAnimation)
    {
        _fairySimpleMove = fairySimpleMove;
        _fairyAttack = fairyAttack;
        _fairyAnimation = fairyAnimation;
    }

    private void OnEnable()
    {
        _fairyAttack.OnPrepareAttack += OnPrepareAttack;
        _dangerZone.OnEnter += VisiblePlayer;
    }

    private void OnDisable()
    {
        _dangerZone.OnEnter -= VisiblePlayer;
        _fairySimpleMove.OnDisable();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isAlife = true;
        _fairySimpleMove.Start(_ease, _direction, _duration, transform);
        _fairyAttack.Start(_timeWaitAttack,transform);
    }

    public void TakeDamage(float damage)
    {
        if (!_isAlife) { return; }
        OnDead();
    }

    private void OnPrepareAttack()
    {
        if (!_isAlife) { return; }
        _fairySimpleMove.OnPause(true);
        _fairySimpleMove.IdleStay(OnAttack);
    }

    private void OnAttack()
    {
        if (!_isAlife) { return; }
        _fairyAttack.Attack();
        _fairySimpleMove.OnPause(false);
    }

    private void VisiblePlayer(Collider collider)
    {
        if (!_isAlife) { return; }
        _fairyAttack.TryAttack(collider);
    }

    private void OnDead()
    {
        _isAlife = false;
        _fairyAnimation.OnDead(_animator);
        _rigidbody.isKinematic = false;
        _collider.isTrigger = false;
        _dangerZone.enabled = false;
        _fairySimpleMove.OnDisable();
        _particleSystem.Play();
        Destroy(gameObject,3f);
    }
}
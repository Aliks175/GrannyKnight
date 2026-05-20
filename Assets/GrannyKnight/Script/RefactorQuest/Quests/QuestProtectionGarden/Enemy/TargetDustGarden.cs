using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class TargetDustGarden : MonoBehaviour, IHealtheble
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private DangerZone _dangerZone;
    [SerializeField] private Color _hitColor;
    [SerializeField] private float _timeWaitTouch;
    [SerializeField] private float _damage;
    [SerializeField] private int _maxTouch;
    private DustAttack _attackDust;
    private MoveToPlayer _moveToPlayer;
    private NavMeshAgent _agent;
    private Sequence _sequence;
    private bool _isAlife;

    private Color _startColor;

    [Inject]
    public void Construct(MoveToPlayer moveToPlayer, DustAttack attackMonsterGarden)
    {
        _moveToPlayer = moveToPlayer;
        _attackDust = attackMonsterGarden;
    }

    private void OnDisable()
    {
        _moveToPlayer.Dispose();
        _attackDust.Dispose();
    }

    private void OnEnable()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        _startColor = _sprite.color;
        _attackDust.OnAttack += OnAttack;
        _attackDust.OnEndAttack += OnEndAttack;
        _moveToPlayer.Initialization(_agent);
    }

    private void OnAttack()
    {
        Debug.Log($"OnAttack / _isAlife = {_isAlife}");
        if (!_isAlife) { return; }
        _isAlife = false;
        _attackDust.Attack();
    }

    private void Start()
    {
        if (_agent == null) { return; }
        _isAlife = true;
        _moveToPlayer.StartMove();
        _attackDust.Start(_dangerZone, transform, _maxTouch, _timeWaitTouch, _damage);
    }

    public void TakeDamage(float damage)
    {
        if (!_isAlife) { return; }
        _isAlife = false;

        _sequence = DOTween.Sequence();
        _sequence.Append(
            _sprite.DOColor(_hitColor, 0.1f)
            .From(_startColor)
            .SetLoops(10));

        _sequence.Join(
            _sprite.gameObject.transform.DOScale(0.1f, 1f)
            .SetLink(gameObject)
            .OnComplete(() => EndLife()));

        _sequence.Play();
    }

    private void OnEndAttack()
    {
        EndLife();
    }

    private void EndLife()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }

}
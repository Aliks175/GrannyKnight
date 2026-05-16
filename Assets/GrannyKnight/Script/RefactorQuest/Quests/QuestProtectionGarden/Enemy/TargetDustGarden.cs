using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class TargetDustGarden : MonoBehaviour, IHealtheble
{
    [SerializeField] private DangerZone _dangerZone;
    [SerializeField] private float _timeWaitTouch;
    [SerializeField] private int _maxTouch;
    private DustAttack _attackDust;
    private MoveToPlayer _moveToPlayer;
    private NavMeshAgent _agent;

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
        _moveToPlayer.Initialization(_agent);
    }

    private void Start()
    {
        if (_agent == null) { return; }
        _moveToPlayer.StartMove();
        _attackDust.Start(_dangerZone,transform, _maxTouch, _timeWaitTouch);
    }

    public void TakeDamage(float damage)
    {
        gameObject.SetActive(false);
    }
}
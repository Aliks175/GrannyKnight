using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class TargetBossGarden : TargetMonsterGarden, IHealtheble
{
    private MoveToPlayer _moveToPlayer;
    private NavMeshAgent _agent;

    [Inject]
    public void Construct(MoveToPlayer moveToPlayer)
    {
        _moveToPlayer = moveToPlayer;
    }

    private void OnDisable()
    {
        _moveToPlayer.Dispose();
    }

    private void OnEnable()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        //_attackMonsterGarden.Start();
    }

    private void Start()
    {
        if (_agent == null) { return; }
        _moveToPlayer.Initialization(_agent);
        _moveToPlayer.StartMove();
    }

    public override void OnDead()
    {
        _moveToPlayer.Dispose();
        base.OnDead();
    }
}
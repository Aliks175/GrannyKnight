using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowPlayer : MonoBehaviour
{
    private NavMeshAgent _agent;
    private MoveToPlayer _moveToPlayer;

    [Inject]
    public void Construct(MoveToPlayer moveToPlayer)
    {
        _moveToPlayer = moveToPlayer;
    }

    private void Awake()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Start()
    {
        if (_agent == null) { return; }
        _moveToPlayer.Initialization(_agent);
        _moveToPlayer.StartMove();
    }

}
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class Wisp : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    public void MoveToTarget(Transform _target)
    {
        if (_agent != null && _target != null)
        {
            _agent.SetDestination(_target.position);
            DestroyAtEnd().Forget();
        }
    }
    private async UniTaskVoid DestroyAtEnd()
    {
        await UniTask.WaitUntil(() => _agent.remainingDistance <= 0.1f);
        Destroy(gameObject);
    }
}

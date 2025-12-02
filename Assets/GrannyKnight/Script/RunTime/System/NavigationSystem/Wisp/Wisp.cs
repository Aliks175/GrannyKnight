using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Wisp : MonoBehaviour
{
    private NavMeshAgent _agent;

    public void Initialization()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget(Transform _target)
    {

        if (_agent != null && _target != null)
        {
            _agent.destination = _target.position;
            DestroyAtEnd().Forget();
        }
    }

    private async UniTaskVoid DestroyAtEnd()
    {
        await UniTask.WaitUntil(() => _agent.remainingDistance <= 0.3f);
        await UniTask.WaitForSeconds(3 * 1000);
        gameObject.SetActive(false);
    }
}
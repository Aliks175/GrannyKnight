using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Wisp : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Vector3 _position;
    private WaitForSeconds _wait;
    private Coroutine _coroutineWaitTime;
    private Coroutine _coroutineWaitDistance;

    private void OnDisable()
    {
        Stop();
        _agent.isStopped = true;
    }

    public void Initialization()
    {
        _agent = GetComponent<NavMeshAgent>();
        _wait = new WaitForSeconds(3f);
    }

    public void MoveToTarget(Transform _target)
    {
        _position = _target.position;

        Stop();
        if (_agent != null && _target != null)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_position);
            _coroutineWaitDistance = StartCoroutine(WaitDistance());
        }
    }

    private IEnumerator WaitDistance()
    {
        yield return new WaitUntil(() => _agent.hasPath);
        yield return new WaitWhile(() => _agent.remainingDistance >= 1f);
        _coroutineWaitTime = StartCoroutine(WaitTime());
    }

    private IEnumerator WaitTime()
    {
        yield return _wait;
        //Debug.Log($"Disable");
        gameObject.SetActive(false);
    }

    private void Stop()
    {
        if (_coroutineWaitTime != null)
        {
            StopCoroutine(_coroutineWaitTime);
        }
        if (_coroutineWaitDistance != null)
        {
            StopCoroutine(_coroutineWaitDistance);
        }
    }
}
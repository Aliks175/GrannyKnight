using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayer : IDisposable
{
    private NavMeshAgent _agent;
    private SystemBuss _systemBuss;
    private PlayerCharacter _player;
    private CancellationToken _waitStayToken;
    private Vector3 _targetPoint;
    private float _stopDistance = 1.5f;
    private bool _isStopped;

    private MoveToPlayer(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    public void Dispose()
    {
       
        //_waitStayToken?.Cancel();
        //_waitStayToken?.Dispose();
        //_waitStayToken = null;
        if (_agent != null)
        {
        _agent.isStopped = true;
        ControlMove(false);
        }
    }

    public void Initialization(NavMeshAgent navMeshAgent)
    {
        _agent = navMeshAgent;
        _agent.stoppingDistance = _stopDistance;
        _waitStayToken = _agent.gameObject.GetCancellationTokenOnDestroy();
        //_waitStayToken?.Cancel();
        //_waitStayToken?.Dispose();
        //_waitStayToken = new CancellationTokenSource();
    }

    public void StartMove()
    {
        ControlMove(true);
        WaitPlayer().Forget();
    }

    public void StopMove()
    {
        ControlMove(false);
    }

    private async UniTaskVoid Move(CancellationToken cancellationToken)
    {
        while (_isStopped)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken:cancellationToken);
            if (CheckDistance())
            {
                Debug.Log($"Move");
                Move(_player.transform.position);
            }
        }
    }

    private async UniTaskVoid WaitPlayer()
    {
        if (CheckPlayer())
        {
            PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
            _player = playerCharacter;
        }
        if (CheckPlayer()) { return; }
    
        Move(_waitStayToken).Forget();
    }

    private bool CheckPlayer()
    {
        return _player == null;
    }

    private void ControlMove(bool isMoved)
    {
        _isStopped = isMoved;
    }

    private void Move(Vector3 retreatPoint)
    {
        if (!ControlAgent()) { return; }
        NavMeshPath path = new NavMeshPath();
        if (!BuildPath(retreatPoint, path, out NavMeshHit hit)) { return; }
        _agent.SetDestination(hit.position);
    }

    private bool ControlAgent()
    {
        bool isPathComplete = true;

        if (!_isStopped)
        {
            isPathComplete = false;
        }
        else if (!_agent.isActiveAndEnabled || !_agent.gameObject.activeSelf)
        {
            isPathComplete = false;
        }
        else if (!WarpAgent())
        {
            isPathComplete = false;
        }

        return isPathComplete;
    }

    protected bool BuildPath(Vector3 retreatPoint, NavMeshPath path, out NavMeshHit hit)
    {
        hit = default;
        bool isPathComplete = false;

        if (!_agent.isOnNavMesh)
        {
            Debug.LogError($"isOnNavMesh = {_agent.isOnNavMesh}");
            return false;
        }

        if (NavMesh.SamplePosition(retreatPoint, out hit, 2f, NavMesh.AllAreas))
        {
            if (_agent.CalculatePath(hit.position, path))
            {
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    //Debug.Log("NavMeshPathStatus.PathComplete");
                    isPathComplete = true;
                }
            }
        }
        return isPathComplete;
    }

    protected bool WarpAgent()
    {
        bool iscorrectAgent = _agent.isOnNavMesh;
        if (iscorrectAgent) return true;
        if (NavMesh.SamplePosition(_agent.transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            _agent.Warp(hit.position);
        }
        iscorrectAgent = _agent.isOnNavMesh;
        return iscorrectAgent;
    }

    private bool CheckDistance()
    {
        if (_agent == null) return false;
        Vector3 durection = _agent.transform.position - _player.transform.position;
        float distance = durection.magnitude;

        Debug.Log($"distance = {distance}");
        Debug.Log($"CheckDistance = {_stopDistance < distance}");
        return _stopDistance < distance;
    }
}
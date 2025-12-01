using UnityEngine;
using UnityEngine.AI;

public class Wisp : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private NavMeshAgent _agent;
    
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    public void MoveToTarget()
    {
        Debug.Log("Agent:" + _agent);
        Debug.Log("Target:" + _target);
        if (_agent != null && _target != null)
        {
            _agent.SetDestination(_target.position);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MoveToTarget();
        }
    }
}

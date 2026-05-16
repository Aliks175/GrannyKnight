using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class DangerZone : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _timeWaitAttack;
    [SerializeField] private LayerMask _layerPlayer;
    private Collider[] _player;
    private WaitForSeconds _waitAttack;
    private SphereCollider _collider;
    private Coroutine _coroutine;
    private int _countPlayer;

    public event Action<Collider> OnEnter;

    private void Awake()
    {
        _waitAttack = new WaitForSeconds(_timeWaitAttack);
        _collider = GetComponent<SphereCollider>();
        _collider.radius = _radius;
        _player = new Collider[1];
    }

    public bool CheckPlayer(Vector3 position, float radius)
    {
        _countPlayer = Physics.OverlapSphereNonAlloc(position, radius, _player, _layerPlayer);
        return _countPlayer > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckPlayer(transform.position, _radius))
        {
            //Debug.Log("OnTriggerEnter");
            OnEnter?.Invoke(other);
            //_coroutine = StartCoroutine(WaitAttack(other));
           StartCoroutine(WaitAttack(other));
        }
    }

    private IEnumerator WaitAttack(Collider other)
    {
        yield return _waitAttack;
        if (CheckPlayer(transform.position, _radius))
        {
            //Debug.Log("WaitAttack");
            OnEnter?.Invoke(other);
            //_coroutine = StartCoroutine(WaitAttack(other));
            StartCoroutine(WaitAttack(other));
        }
    }
}
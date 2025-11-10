using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TargetFairy : MonoBehaviour, IHealtheble
{
    [SerializeField] private List<Transform> m_Targets;
    [SerializeField] private Ease _ease;
    [SerializeField, Range(0.1f, 10)] private float _speed;
    [SerializeField] private int _startCount;
    [SerializeField] private bool _isPlay = true;
    private int _count;
    private int _index;
    private float _position;
    private float _tempPosition;
    private Tween _tween;

    private void Start()
    {
        if (!_isPlay) { return; }
        _count = _startCount;
        Move();
    }

    public void TakeDamage(float damage)
    {
        _isPlay = !_isPlay;
    }

    private void ChangeTarget()
    {
        FindPath();
        if (_count - 1 > 0)
        {
            _count--;
        }
        else
        {
            Debug.Log("Over");
            _count = _startCount;
        }
        Move();
    }

    private void Move()
    {
        _tween = transform.DOMove(m_Targets[_index].position, _speed).SetSpeedBased().SetEase(_ease).OnComplete(() => ChangeTarget());
        _tween.Play();
    }

    private void FindPath()
    {
        _index = Random.Range(0, m_Targets.Count);
    }
}
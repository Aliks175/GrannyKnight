using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetFairy : MonoBehaviour, IHealtheble
{
    [SerializeField] private Ease _ease;
    private List<Transform> _targetsPoints;
    private Transform _tempTarget;
    private Transform _finalTarget;
    private Coroutine _coroutine;
    private Tween _tween;
    private float _timeWait;
    private float _speed;
    private int _count;
    private int _index;
    private bool _isDolly;
    private bool _isPlay;

    public event Action OnEnd;

    private void OnDisable()
    {
        MainSystem.OnUpdate -= OnUpdate;
    }

    public void Instantiate(FairyType fairyType, FairyTargets fairyTargets)
    {
        _count = fairyType.ValueChangeTarget;
        _timeWait = fairyType.TimeWait;
        _speed = fairyType.Speed;
        _isDolly = fairyType.IsDolly;
        _targetsPoints = fairyTargets.TargetsPoints;
        _finalTarget = fairyTargets.FinalTarget;
    }

    public void Play()
    {
        ChangeTarget();
        if (_isDolly)
        {
            MainSystem.OnUpdate += OnUpdate;
            _isPlay = true;
            _coroutine = StartCoroutine(WaitFinalDashToTarget());
        }
        else
        {
            MoveDashToTarget();
        }
    }

    public void TakeDamage(float damage)
    {
        End();
    }

    private void OnUpdate()
    {
        MoveFollowToTarget();
    }

    private void CheckCompliteMove()
    {
        FindPath();
        if (_count - 1 > 0)
        {
            _count--;
            Debug.Log($"- count {_count}");
        }
        else
        {
            _isPlay = false;
            _tween = transform.DOMove(_finalTarget.position, 2).SetSpeedBased().SetEase(_ease).OnComplete(() => End());
            _tween.Play();
            Debug.Log("Over");
            return;
        }
        ChangeTarget();
        MoveDashToTarget();
    }

    private void ChangeTarget()
    {
        // если фея в тележке то её перемещение работает через лерп Vector 3
        // Если она прыгает по точкам то через твины 
        _tempTarget = _targetsPoints[_index];
        Debug.Log("ChangeTarget");
    }

    private void MoveDashToTarget()
    {
        _tween = transform.DOMove(_tempTarget.position, _speed).SetSpeedBased().SetEase(_ease).OnComplete(() => CheckCompliteMove());
        _tween.Play();
    }

    private void MoveFollowToTarget()
    {
        if (!_isPlay) return;
        transform.position = Vector3.LerpUnclamped(transform.position, _tempTarget.position, _speed * Time.deltaTime);
    }

    private IEnumerator WaitFinalDashToTarget()
    {
        yield return new WaitForSeconds(_timeWait);
        _isPlay = false;
        _count = 0;
        CheckCompliteMove();
    }

    private void FindPath()
    {
        _index = Random.Range(0, _targetsPoints.Count);
    }

    private void End()
    {
        OnEnd?.Invoke();
        _tween.Kill();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _isPlay = false;
        Destroy(gameObject);
    }
}
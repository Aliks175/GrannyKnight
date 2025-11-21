using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fairy : MonoBehaviour, IHealtheble
{
    [SerializeField] private Ease _ease;
    [SerializeField] private float _timerPickUp;
    [SerializeField] private float _bazeOffsetHeight;
    [SerializeField, Range(0.1f, 2f)] private float _coefficientSpeedWithItem;
    [SerializeField, Range(0.1f, 2f)] private float _coefficientSpeedWithoutItem;
    private List<Transform> _movePoints;
    private FairyCreater _fairyCreater;
    private FairyItem _fairyItem;
    private Transform _activeTarget;
    private Transform _finalTarget;
    private Coroutine _coroutine;
    private WaitForSeconds _waitDash;
    private WaitForSeconds _waitTimer;
    private Tween _tween;
    private float _timeWait;
    private float _speed;
    private int _count;
    private int _index;
    private bool _isFollow;
    private bool _isPlay;
    public event Action OnEnd;

    private void OnDisable()
    {
        MainSystem.OnUpdate -= OnUpdate;
    }

    public void Instantiate(FairyType fairyType, FairyTargets fairyTargets, FairyCreater fairyCreater)
    {
        _count = fairyType.ValueChangeTarget;
        _timeWait = fairyType.TimeWait;
        _speed = fairyType.Speed;
        _isFollow = fairyType.IsFollow;
        _movePoints = fairyTargets.MovePoints;
        _finalTarget = fairyTargets.FinalTarget;
        _fairyCreater = fairyCreater;
        _waitDash = new WaitForSeconds(_timeWait);
        _waitTimer = new WaitForSeconds(_timerPickUp);
    }

    public void TakeDamage(float damage)
    {
        if (_fairyItem != null)
        {
            _fairyItem.DropItem(transform.position);
        }
        End();
    }

    public void Play()
    {
        ChangeTarget();
        if (_isFollow)
        {
            MainSystem.OnUpdate += OnUpdate;
            _isPlay = true;
            _coroutine = StartCoroutine(WaitDashToTarget());
        }
        else
        {
            MoveDashToTarget();
        }
    }

    private void CheckCompliteMove()
    {
        if (_count - 1 > 0)
        {
            _count--;
        }
        else
        {
            if (_fairyItem == null)
            {
                _fairyItem = _fairyCreater.GetFairyTarget();
                _fairyItem.SetFairy();
                _count = 2;
                CheckCompliteMove();
                return;
            }
            Vector3 target = _fairyItem.transform.position;
            target.y += _bazeOffsetHeight;

            if (target.y < transform.position.y)
            {
                _tween = transform.DOMoveY(target.y, _speed).SetEase(_ease).OnComplete(() => CheckCompliteMove());
                _tween.Play();
                return;
            }
            _tween = transform.DOMove(target, _speed * _coefficientSpeedWithoutItem).SetEase(Ease.InSine).OnComplete(() => StartTimer());
            _tween.Play();
            return;
        }
        FindPath();
        ChangeTarget();
        MoveDashToTarget();
    }

    private void OnUpdate()
    {
        MoveFollowToTarget();
    }

    private void StartTimer()
    {
        _fairyItem.StartProgressPickUp();
        _coroutine = StartCoroutine(WaitTimer());
    }

    #region FindPath
    private void ChangeTarget()
    {
        _activeTarget = _movePoints[_index];
    }

    private void FindPath()
    {
        _index = Random.Range(0, _movePoints.Count);
    }
    #endregion

    #region Move
    private void MoveDashToTarget()
    {
        _tween = transform.DOMove(_activeTarget.position, _speed * _coefficientSpeedWithoutItem).SetEase(_ease).OnComplete(() => CheckCompliteMove());
        _tween.Play();
    }

    private void MoveFollowToTarget()
    {
        if (!_isPlay) return;
        transform.position = Vector3.LerpUnclamped(transform.position, _activeTarget.position, _speed * Time.deltaTime);
    }
    #endregion

    #region Wait
    private IEnumerator WaitDashToTarget()
    {
        yield return _waitDash;
        _isPlay = false;
        _count = 0;
        CheckCompliteMove();
    }

    private IEnumerator WaitTimer()
    {
        yield return _waitTimer;
        _fairyItem.PickUp();
        _tween = transform.DOMove(_finalTarget.position, _speed * _coefficientSpeedWithItem).SetEase(Ease.InSine).OnComplete(() => FairyRanAway());
        _tween.Play();
    }
    #endregion

    private void FairyRanAway()
    {
        _fairyItem.LostItem();
        End();
    }

    public void End()
    {
        OnEnd?.Invoke();
        _tween?.Kill();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _isPlay = false;
        Destroy(gameObject);
    }
}
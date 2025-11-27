using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Fairy : MonoBehaviour, IHealtheble
{
    [SerializeField] private Ease _ease;
    [SerializeField] private float _timerPickUp;
    [SerializeField] private float _bazeOffsetHeight;
    [SerializeField, Range(0.1f, 10f)] private float _coefficientSpeedWithItem;
    [SerializeField, Range(0.01f, 1f)] private float _coefficientSpeedWithoutItem;
    [SerializeField] private ParticleSystem _particleSystem;
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
    private bool _isDead = false;
    public event Action OnEndGame;

    public UnityEvent OnStart;
    public UnityEvent OnEnd;
    public UnityEvent _pickUpevent;
    public UnityEvent _changeTarget;
    public UnityEvent _dieEvent;

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
        if (_isDead) return;
        if (_fairyItem != null)
        {
            _fairyItem.DropItem(transform.position);
        }
        _isDead = true;
        End();
    }

    public void Play()
    {
        OnStart?.Invoke();
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
                _count = 5;
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
        _changeTarget?.Invoke();
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
        transform.LookAt(_activeTarget);
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
        _pickUpevent?.Invoke();
        _tween = transform.DOMove(_finalTarget.position, _speed * _coefficientSpeedWithItem).SetEase(Ease.InSine).OnComplete(() => FairyRanAway());
        _tween.Play();
    }
    #endregion

    private void FairyRanAway()
    {
        _fairyItem.LostItem();
        _isDead = false;
        End();
    }

    public void End()
    {
        _fairyCreater.CheckLiveEnemy(this);
        OnEnd?.Invoke();
        _tween?.Kill();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _isPlay = false;
        if (_isDead)
        {
            _dieEvent?.Invoke();
            _particleSystem.Play();
            GetComponent<SphereCollider>().isTrigger = false;
            GetComponent<Rigidbody>().isKinematic = false;
            Destroy(gameObject, 3f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public void GameEnd()
    //{
    //    OnEnd?.Invoke();
    //    OnEndGame?.Invoke();
    //    _tween?.Kill();
    //    if (_coroutine != null)
    //    {
    //        StopCoroutine(_coroutine);
    //    }
    //    _isPlay = false;
    //    Destroy(gameObject);
    //}
}
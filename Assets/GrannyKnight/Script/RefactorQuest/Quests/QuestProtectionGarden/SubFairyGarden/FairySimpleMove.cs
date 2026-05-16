using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FairySimpleMove
{
    private Transform _transform;
    protected Tween _simpleMove;
    
    private Vector3 _startPosition;
    private Sequence _idleStay;
    private Vector3 _direction;
    private Ease _ease;
    private float _duration;
    private float _speed;

    public void OnDisable()
    {
        _simpleMove?.Kill();
        _idleStay?.Kill();
    }

    public void Start(Ease ease, Vector3 direction, float duration, Transform transform)
    {
        _ease = ease;
        _direction = direction;
        _duration = duration;
        _transform = transform;
        RundomRange().Forget();
    }


    private void SimpleLoopMove()
    {
        if (_simpleMove == null)
        {
            CreateTween(_transform);
        }
        _simpleMove.Restart();
    }

    public void IdleStay(Action action)
    {
        _idleStay = DOTween.Sequence(_transform);
        _idleStay.Append(_transform.DOScale(1.2f, 0.8f)
           .SetLoops(2, LoopType.Yoyo)
          .From(Vector3.one))
            .OnComplete(() => action?.Invoke());

        //_idleStay.Join(_transform.DOMoveY(_transform.position.y + 1, 2)
        //     .SetLoops(2, LoopType.Yoyo))
        //    .OnComplete(()=>_transform.position = _startPosition);

        _idleStay.Play();
    }


    private void CreateTween(Transform transform)
    {
        _simpleMove = transform.DOMove(transform.position + _direction, _speed)
          .SetLoops(-1, LoopType.Yoyo)
          .SetEase(_ease)
          .SetAutoKill(false);
    }

    private async UniTask RundomRange()
    {
        _speed = Random.Range(0, 1f);
        await UniTask.Delay(System.TimeSpan.FromSeconds(_speed));
        _speed = +_duration;
        SimpleLoopMove();
    }

    public void OnPause(bool _isActivePause)
    {
        if (_isActivePause)
        {
            _simpleMove.Pause();
            //IdleStay();
        }
        else
        {
            //_idleStay?.Complete(true);
            _simpleMove.Play();
        }
    }
}
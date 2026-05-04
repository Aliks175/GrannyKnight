using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [Header("SimpleMove")]
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _duration;
    private Tween _tween;
    private float _speed;

    private void Start()
    {
        RundomRange().Forget();
    }

    private void OnDisable()
    {
        _tween?.Kill();
    }

    private void SimpleLoopMove()
    {
        _tween = transform.DOMove(transform.position + _direction, _speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine);
        _tween.Play();
    }

    private async UniTask RundomRange()
    {
        _speed = Random.Range(0, 1f);
        await UniTask.Delay(System.TimeSpan.FromSeconds(_speed));
        _speed = +_duration;
        SimpleLoopMove();
    }
}

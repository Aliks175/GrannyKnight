using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [Header("SimpleMove")]
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _duration;
    private float _speed;

    private void Start()
    {
        RundomRange().Forget();
    }

    private void SimpleLoopMove()
    {
        Tween tween = transform.DOMove(transform.position + _direction, _speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine);
        tween.Play();
    }
    
    private async UniTask RundomRange()
    {
        _speed = Random.Range(0, 1f);
        await UniTask.Delay(System.TimeSpan.FromSeconds(_speed));
        _speed =+ _duration;
        SimpleLoopMove();
    }
}

using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [Header("SimpleMove")]
    [SerializeField] private Ease _ease;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _duration;
    protected Tween _tween;
    private float _speed;

    private void OnDisable()
    {
        _tween?.Kill();
    }

    private void Start()
    {
        RundomRange().Forget();
    }

    private void SimpleLoopMove()
    {
        if (_tween == null)
        {
            _tween = transform.DOMove(transform.position + _direction, _speed)
           .SetLoops(-1, LoopType.Yoyo)
           .SetEase(_ease)
           .SetAutoKill(false);
        }
        _tween.Restart();
    }
    
    private async UniTask RundomRange()
    {
        _speed = Random.Range(0, 1f);
        await UniTask.Delay(System.TimeSpan.FromSeconds(_speed));
        _speed =+ _duration;
        SimpleLoopMove();
    }
}

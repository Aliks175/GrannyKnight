using DG.Tweening;
using UnityEngine;

public class ObjectForQuest : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private Vector3 _moving;
    [SerializeField] private float _timeForLoop;
    [SerializeField] private Ease _easeMoving;
    [Header("Rotate")]
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _rotationTimeForLoop;
    [SerializeField] private Ease _easeRotate;
    [SerializeField] private bool _isDebug = false;
    private Tween _moveTween;
    private Tween _rotationTween;
    void Awake()
    {
        if (_isDebug) StartMove();
    }
    public void StartMove()
    {
        _moveTween = transform.DOMove(transform.position + _moving, _timeForLoop).SetEase(_easeMoving).SetLoops(-1, LoopType.Yoyo);
        _rotationTween = transform.DORotate(_rotation, _rotationTimeForLoop).SetEase(_easeRotate).SetLoops(-1, LoopType.Incremental);
        _moveTween.Play();
        _rotationTween.Play();
    }
    private void OnDisable()
    {
        _moveTween?.Kill();
        _rotationTween?.Kill();
    }
}

using UnityEngine;
using DG.Tweening;

public class HintButton : MonoBehaviour
{
    [SerializeField] private float _timeToScale;
    [SerializeField] private Vector2 _scale;
    [SerializeField] private Ease _ease;
    private Tween _tween;
    private void StartScale()
    {
        _tween = transform.DOScale(_scale, _timeToScale).SetLoops(-1, LoopType.Yoyo).SetEase(_ease);
        _tween.Play();
    }
    void OnEnable()
    {
        StartScale();
    }
    void OnDisable()
    {
        _tween.Kill();
    }
}

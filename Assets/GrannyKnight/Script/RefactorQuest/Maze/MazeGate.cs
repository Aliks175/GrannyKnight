using UnityEngine;
using DG.Tweening;

public class MazeGate : MonoBehaviour
{
    //[SerializeField] private float _moveY = 3f;
    [SerializeField] private float _moveDuration = 1f;
    [SerializeField] private Ease _moveEase = Ease.InOutCubic;
    private GameObject _gate;
    private Tween _tween;
    private float _Yposition;
    private float _YpositionStart;

    void Awake()
    {
        _gate = transform.GetChild(0).gameObject;
        _Yposition = _gate.GetComponent<Collider>().bounds.size.y;
        _YpositionStart = _gate.transform.position.y;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_tween != null && _tween.IsPlaying())
            {
                _tween.Kill();
            }
            _tween = _gate.transform.DOMoveY(-_Yposition, _moveDuration).SetEase(_moveEase);
            _tween.Play();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_tween != null && _tween.IsPlaying())
            {
                _tween.Kill();
            }
            _tween = _gate.transform.DOMoveY(_YpositionStart, _moveDuration).SetEase(_moveEase);
            _tween.Play();
        }
    }
}

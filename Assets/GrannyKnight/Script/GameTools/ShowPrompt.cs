using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class ShowPrompt : MonoBehaviour
{
    [SerializeField] private float _duration;
    private TextMeshPro _text;
    private Color _startColor;
    private Color _notVisibleColor;
    private Tween _tween;

    private void OnDisable()
    {
        OffTween();
    }

    private void Start()
    {
        _text = GetComponent<TextMeshPro>();
        _startColor = _text.color;
        _notVisibleColor = _startColor;
        _notVisibleColor.a = 0;
        _text.color = _notVisibleColor;
    }

    public void ControlShow(bool isVisible)
    {
        if (isVisible)
        {
            OffTween();
            _tween = _text.DOColor(_startColor, _duration);
            _tween.Play();
        }
        else
        {
            OffTween();
            _tween = _text.DOColor(_notVisibleColor, _duration/2);
            _tween.Play();
        }
    }

    private void OffTween()
    {
        if(_tween != null )
        {
            _tween?.Kill();
        }
    }
}

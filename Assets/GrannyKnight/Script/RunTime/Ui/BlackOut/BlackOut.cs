using DG.Tweening;
using UnityEngine;

public class BlackOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup _BlackPanel;
    private Sequence _sequence;
    private Tween _tweenOnBlack;
    private Tween _tweenOFFBlack;

    private void Start()
    {
    }

    public void Active()
    {
        _tweenOnBlack = DOTween.To(() => _BlackPanel.alpha, x => _BlackPanel.alpha = x, 1f, 0);
        _tweenOFFBlack = DOTween.To(() => _BlackPanel.alpha, x => _BlackPanel.alpha = x, 0f, 0.8f);
        _sequence = DOTween.Sequence();
        _sequence.Append(_tweenOnBlack);
        _sequence.Append(_tweenOFFBlack);
        _sequence.Play();
    }
}
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class UiTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private BlackOut _blackOut;
    private Sequence _sequence;

    public void StartTimerGame(Action action)
    {
        //Таймер появляется 3, 2 , 1 , во время появления цифра раздувается из маленькой сменяется цифра,
        //сново маленькая через From и она снова увеличивается
        //после проигрывания анимации запуск мини игры 
        _textTimer.gameObject.SetActive(true);
        _sequence = DOTween.Sequence();
        _textTimer.text = "3";
        _sequence.Append(_textTimer.gameObject.transform.DOScale(5, 1).From(0).OnComplete(() => _textTimer.text = "2"));
        _sequence.Append(_textTimer.gameObject.transform.DOScale(5, 1).From(0).OnComplete(() => _textTimer.text = "1"));
        _sequence.Append(_textTimer.gameObject.transform.DOScale(5, 1).From(0).OnComplete(() => _textTimer.gameObject.SetActive(false)));
        _sequence.OnComplete(() => action?.Invoke());
        _sequence.Play();
    }

    public void Stop()
    {
        Debug.Log($"UiThree - Stop");
        _textTimer.gameObject.SetActive(false);
        Debug.Log($"_panelUi - {_textTimer.gameObject.activeSelf}");
        _blackOut.Active();
    }
}

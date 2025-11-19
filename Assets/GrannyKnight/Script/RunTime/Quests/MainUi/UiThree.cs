using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class UiThree : MonoBehaviour
{
    [SerializeField] private GameObject _panelUi;
    [SerializeField] private TextMeshProUGUI _textProgressQuest;
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private BlackOut _blackOut;
    private int StartcountEnemy;
    private int StartcountItem;
    private Sequence _sequence;
    private string _textDiscription;

    public void Initialization(int countEnemy, int countItem)
    {
        StartcountEnemy = countEnemy;
        StartcountItem = countItem;
    }

    public void StartTimerGame(Action action)
    {
        //Таймер появляется 3, 2 , 1 , во время появления цифра раздувается из маленькой сменяется цифра,
        //сново маленькая через From и она снова увеличивается
        //после проигрывания анимации запуск мини игры 
        _panelUi.SetActive(true);
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
        _panelUi.SetActive(false);
        Debug.Log($"_panelUi - {_panelUi.activeSelf}");
        _blackOut.Active();
    }

    public void OnUpdateUi(int countEnemy, int countItem)
    {
        _textDiscription = $"Кол-во Рулетов : {countItem} / {StartcountItem}\n\nКол-во Пикси : {countEnemy} / {StartcountEnemy}";
        _textProgressQuest.SetText(_textDiscription);
    }
}

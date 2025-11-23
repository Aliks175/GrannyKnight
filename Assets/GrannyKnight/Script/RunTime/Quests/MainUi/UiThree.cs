using System;
using TMPro;
using UnityEngine;

public class UiThree : MonoBehaviour
{
    [SerializeField] private GameObject _panelUi;
    [SerializeField] private TextMeshProUGUI _textProgressQuest;
    [SerializeField] private UiTimer _uiTimer;
    private int _startcountEnemy;
    private int _startcountItem;

    public void Initialization(int countEnemy, int countItem)
    {
        _startcountEnemy = countEnemy;
        _startcountItem = countItem;
    }

    public void StartTimerGame(Action action)
    {
        _panelUi.SetActive(true);
        _uiTimer.StartTimerGame(action);
    }

    public void Stop()
    {
        _panelUi.SetActive(false);
        _uiTimer.Stop();
    }

    public void OnUpdateUi(int countEnemy, int countItem)
    {
       string textDiscription = $"{countEnemy} / {_startcountEnemy}\n\n\n\n{countItem} / {_startcountItem}";
        _textProgressQuest.SetText(textDiscription);
    }
}
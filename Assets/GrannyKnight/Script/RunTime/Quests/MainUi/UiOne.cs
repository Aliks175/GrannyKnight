using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiOne : MonoBehaviour
{
    [SerializeField] private GameObject _panelUi;
    [SerializeField] private Slider _sliderProgress;
    [SerializeField] private UiTimer _uiTimer;

    public void Initialization(float MaxHealth)
    {
        _sliderProgress.maxValue = MaxHealth;
        _sliderProgress.value = MaxHealth;
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

    public void OnUpdateUi(float enemyHealth)
    {
        _sliderProgress.value = enemyHealth;
        //_textDiscription = $"{countItem} / {StartcountItem}\n\n{countEnemy} / {StartcountEnemy}";
        //_textProgressQuest.SetText(_textDiscription);
    }
}

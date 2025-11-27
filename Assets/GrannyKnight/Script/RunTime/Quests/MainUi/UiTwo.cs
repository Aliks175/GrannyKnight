using System;
using UnityEngine;
using UnityEngine.UI;

public class UiTwo : MonoBehaviour
{
    [SerializeField] private GameObject _panelUi;
    [SerializeField] private UiTimer _uiTimer;
    [SerializeField] private Image _timerUi;
    [SerializeField] private Image _progressUI;
    private float _maxTime;
    private int _maxCountFruit;
    private float _progressForOne;

    public void Initialization(float time, int maxCountFruit)
    {
        _maxTime = time;
        _maxCountFruit = maxCountFruit;
        _progressForOne = 1f / _maxCountFruit;
        _progressUI.fillAmount = 0f;
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

    public void OnUpdateUiProgress(int CountFruit)
    {
        if (CountFruit > _maxCountFruit)
        {
            _progressUI.fillAmount = 1f;
            return;
        }
        _progressUI.fillAmount += _progressForOne;
    }

    public void OnUpdateUiTimer(float time)
    {
        float tempProcentTime = time / _maxTime;
        tempProcentTime = Mathf.Clamp01(tempProcentTime);
        _timerUi.fillAmount = tempProcentTime;
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class UiTwo : MonoBehaviour
{
    [SerializeField] private GameObject _panelUi;
    [SerializeField] private UiTimer _uiTimer;
    [SerializeField] private Image _timerUi;
    [SerializeField] private Slider _sliderProgress;
    private float _maxTime;
    private int _maxCountFruit;

    public void Initialization(float time, int maxCountFruit)
    {
        _maxTime = time;
        _maxCountFruit = maxCountFruit;
        _sliderProgress.maxValue = _maxCountFruit;
        _sliderProgress.value = 0;
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
            _sliderProgress.value = _maxCountFruit;
            return;
        }
        _sliderProgress.value = CountFruit;
    }

    public void OnUpdateUiTimer(float time)
    {
        float tempProcentTime = time / _maxTime;
        tempProcentTime = Mathf.Clamp01(tempProcentTime);
        _timerUi.fillAmount = tempProcentTime;
    }
}

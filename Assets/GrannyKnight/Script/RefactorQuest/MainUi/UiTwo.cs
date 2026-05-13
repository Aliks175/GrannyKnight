using System;
using UnityEngine;
using UnityEngine.UI;

public class UiTwo : MonoBehaviour
{
    [SerializeField] private GameObject _panelUi;
    [SerializeField] private GameObject _panelPrompt;
    [SerializeField] private Image _progressUI;
    private int _maxCountFruit;
    private float _progressForOne;

    public void Initialization(int maxCountFruit)
    {
        _maxCountFruit = maxCountFruit;
        _progressForOne = 1f / _maxCountFruit;
        _progressUI.fillAmount = 0f;
    }

    public void StartTimerGame()
    {
        _panelUi.SetActive(true);
        _panelPrompt.SetActive(true);
        _progressUI.fillAmount = 0f;
    }

    public void Stop()
    {
        _panelUi.SetActive(false);
        _panelPrompt.SetActive(false);
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
}
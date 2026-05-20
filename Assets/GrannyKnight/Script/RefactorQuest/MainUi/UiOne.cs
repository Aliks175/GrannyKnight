using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiOne : MonoBehaviour
{
    [SerializeField] private GameObject _panelUi;
    [SerializeField] private Image _progressUI;
    //[SerializeField] private UiTimer _uiTimer;
    [SerializeField] private GameObject _panelPrompt;
    //[SerializeField] private float _timeDisablePrompt;


    public void Initialization()
    {
        _panelUi.SetActive(false);
        _panelPrompt.SetActive(false);
        //_sliderProgress.maxValue = MaxHealth;
        //_sliderProgress.value = MaxHealth;
        //if(_uiTimer == null)
        //{
        //    _uiTimer = GameObject.FindFirstObjectByType<UiTimer>();
        //}
    }

    public void StartTimerGame()
    {
        _panelUi.SetActive(true);
        _panelPrompt.SetActive(true);
        //_uiTimer.StartTimerGame(action);
        //Invoke(nameof(DisablePrompt), _timeDisablePrompt);
    }

    public void Stop()
    {
        _panelUi.SetActive(false);
        //_uiTimer.Stop();
    }

    public void OnUpdateUi(float enemyHealth)
    {
        _progressUI.fillAmount = enemyHealth;
        //_sliderProgress.value = enemyHealth;
    }

    private void DisablePrompt()
    {
        _panelPrompt.SetActive(false);
    }
}

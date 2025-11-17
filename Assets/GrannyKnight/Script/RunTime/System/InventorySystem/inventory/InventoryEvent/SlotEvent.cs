using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotEvent : MonoBehaviour
{
    public bool IsView => _canvasGroup.alpha > 0.1f;
    [SerializeField] private Image _icon;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _nameItem;
    [SerializeField] private TextMeshProUGUI _countItem;
    private float _timerView;
    private float _speedView;
    private float _speedHide;
    private WaitForSeconds _waitTime;
    private bool _isPlay = false;

    public void Initialization(float timerView, float speedView, float speedHide)
    {
        _timerView = timerView;
        _speedView = speedView;
        _speedHide = speedHide;
        _canvasGroup.alpha = 0;
        _isPlay = false;
        _waitTime = new WaitForSeconds(_timerView);
    }

    /// <summary>
    /// Устанавливаем отоброжение предмета
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(Item item)
    {
        _icon.sprite = item.IconItem;
        _nameItem.SetText(item.Name);
        _countItem.SetText($"x {item.CountItem} ");
        gameObject.SetActive(true);
        _isPlay = true;
        OnView();
    }

    private void OnView()
    {
        StartCoroutine(OnViewEvent(0f, 1f, _speedView, TimerView));
    }

    private void OnHide()
    {
        _isPlay = true;
        StartCoroutine(OnViewEvent(1f, 0f, _speedHide, () => gameObject.SetActive(false)));
    }

    private void TimerView()
    {
        StartCoroutine(WaitTime(OnHide));
    }

    private IEnumerator WaitTime(Action action)
    {
        yield return _waitTime;
        action?.Invoke();
    }

    /// <summary>
    /// Метод плавного изменения отображения уведомления
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="_speedCoefficient"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    private IEnumerator OnViewEvent(float start, float end, float _speedCoefficient, Action action)
    {
        float tempTime = start;
        bool isEnd = start > end;
        int modCoefficient = isEnd ? -1 : 1;
        while (_isPlay)
        {
            yield return null;
            tempTime += modCoefficient * _speedCoefficient * Time.deltaTime;
            if (isEnd && tempTime <= end)
            {
                _isPlay = false;
            }
            else if (!isEnd && tempTime >= end)
            {
                _isPlay = false;
            }
            tempTime = Mathf.Clamp01(tempTime);
            _canvasGroup.alpha = tempTime;
        }
        action?.Invoke();
    }
}
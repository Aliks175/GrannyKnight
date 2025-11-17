using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestViewControl : MonoBehaviour
{
    [SerializeField] private CanvasGroup _questPanel;
    [SerializeField] private TextMeshProUGUI _nameQuest;
    [SerializeField] private TextMeshProUGUI _descriptionQuest;
    [SerializeField, Range(0.8f, 2)] private float SpeedChangeAlpha;

    private Coroutine _coroutine;
    private float _valueAlpha;
    private int _coefficient;
    private bool _isVisible;
    private bool _isPlay;

    public void ShowQuest()
    {
        _isVisible = true;
        _coroutine = StartCoroutine(WaitCanvasGroup());
        Debug.Log("ShowQuest");
    }

    public void HideQuest()
    {
        _isVisible = false;
        _coroutine = StartCoroutine(WaitCanvasGroup());
        Debug.Log("HideQuest");
    }

    public void SetInfo(QuestInfo questInfo)
    {
        _nameQuest.SetText(questInfo.NameQuest);
        _descriptionQuest.SetText(questInfo.Description);
    }

    private IEnumerator WaitCanvasGroup()
    {
        _coefficient = _isVisible ? 1 : -1;
        _isPlay = true;
        while (_isPlay)
        {
            yield return null;
            _valueAlpha += SpeedChangeAlpha * _coefficient * Time.deltaTime;
            _valueAlpha = Mathf.Clamp01(_valueAlpha);
            _questPanel.alpha = _valueAlpha;
            Debug.Log($"_valueAlpha {_valueAlpha}");
            Debug.Log($"_isVisible {_isVisible}");

            Debug.Log($"_isVisible && _valueAlpha >= 0.9f {_isVisible && _valueAlpha >= 0.9f}");
            Debug.Log($"!_isVisible && _valueAlpha <= 0.1f {!_isVisible && _valueAlpha <= 0.1f}");
            if (_isVisible && _valueAlpha >= 0.9f)
            {
                _questPanel.alpha = 1;
                _isPlay = false;
            }
            else if (!_isVisible && _valueAlpha <= 0.1f)
            {
                _questPanel.alpha = 0;
                _isPlay = false;
            }
        }
        _valueAlpha = _questPanel.alpha;
        StopCoroutine(_coroutine);
    }
}
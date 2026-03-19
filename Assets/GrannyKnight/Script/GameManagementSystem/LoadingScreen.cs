using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;
    private const int _maxValue = 100;
    private bool isVisible => _progressBar.value < _maxValue;

    private void Start()
    {
        _progressBar.maxValue = _maxValue;
        _progressBar.wholeNumbers = false;
        _progressBar.value = 0;
    }

    public void ChangeProgressBar(float value)
    {
        //Debug.Log($"value {value}");
        value = Mathf.Abs(value);
        value = _maxValue * value;

        value = value > _maxValue ? _maxValue : value;
        _progressBar.value = value;
        CheckVisible();
    }

    private void CheckVisible()
    {
        gameObject.SetActive(isVisible);
    }
}
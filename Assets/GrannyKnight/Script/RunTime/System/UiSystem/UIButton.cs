using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIButton : MonoBehaviour
{
    [Header("Для анимации кнопки на наведении")]
    [SerializeField] private float _scaleMultiplier = 1.1f;
    [SerializeField] private float _animationDuration = 0.2f;
    [Header("Для анимации кнопки на нажатии")]
    [SerializeField] private float _clickScale = 0.9f;
    [SerializeField] private float _clickDuration = 0.1f;
    private Button _button;
    private Vector3 _originalScale;
    private Tween _tween;
     void Start()
    {
        _button = GetComponent<Button>();
        _originalScale = transform.localScale;
    }

    public void OnPointerEnter()
    {
        if (_button.interactable)
        {
            _tween = transform.DOScale(_originalScale * _scaleMultiplier, _animationDuration);
            _tween.Play();
        }
        Debug.Log("workong");
    }

    public void OnPointerExit()
    {
        _tween = transform.DOScale(_originalScale, _animationDuration);
        _tween.Play();
    }
    public void AnimateClick()
    {
        if (_button.interactable)
        {
            _tween = transform.DOScale(_originalScale * _clickScale, _clickDuration)
                .OnComplete(() => 
                {
                    _tween = transform.DOScale(_originalScale, _clickDuration);
                });
            _tween.Play();
        }
    }

}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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
    private Tween _tweenEnter;
    private Tween _tweenExit;
    private Tween _tweenClick;

    private void Start()
    {
        _button = GetComponent<Button>();
        _originalScale = transform.localScale;
    }

    public void OnPointerEnter()
    {
        if (_tweenEnter.IsActive()) { return; }
        if (_button.interactable)
        {
            _tweenEnter = transform.DOScale(_originalScale * _scaleMultiplier, _animationDuration)
                .From(_originalScale)
                .SetUpdate(true)
                .SetLink(gameObject);
            _tweenEnter.Play();
        }
        //Debug.Log("workong");
    }

    public void OnPointerExit()
    {
        if (_tweenExit.IsActive()) { return; }
        _tweenExit = transform.DOScale(_originalScale, _animationDuration)
            .SetUpdate(true)
             .SetLink(gameObject);
        _tweenExit.Play();
    }

    public void AnimateClick()
    {
        if (_tweenClick.IsActive()) { return; }
        if (_button.interactable)
        {
            _tweenClick = transform.DOScale(_originalScale * _clickScale, _clickDuration)
                .SetLink(gameObject)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    transform.localScale = _originalScale;
                });
            _tweenClick.Play();
        }
    }

}

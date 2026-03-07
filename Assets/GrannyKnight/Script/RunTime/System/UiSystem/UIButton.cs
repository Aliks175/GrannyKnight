using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Для анимации кнопки на наведении")]
    [SerializeField] private float _holdScale = 1.1f;
    [SerializeField] private float _holdDuration = 0.2f;
    [SerializeField] private Color _holdColor = Color.white;
    [Header("Для анимации кнопки на нажатии")]
    [SerializeField] private float _clickScale = 0.9f;
    [SerializeField] private float _clickDuration = 0.1f;
    [SerializeField] private Color _clickColor = Color.gray;
    private Button _button;
    private Image _image;
    private Vector3 _originalScale;
    private Color _originalColor;
    private Tween _scaleTween;
    private Tween _colorTween;
     void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _originalScale = transform.localScale;
        if (_image != null) _originalColor = _image.color;
    }
    void OnEnable()
    {
        if (_originalScale !=  Vector3.zero) transform.localScale = _originalScale;
        if (_image != null) _image.color = _originalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            _scaleTween = transform.DOScale(_originalScale * _holdScale, _holdDuration);
            _scaleTween.Play();
            if (_image != null)
            {
                _colorTween = _image.DOColor(_holdColor, _holdDuration);
                _colorTween.Play();
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _scaleTween = transform.DOScale(_originalScale, _holdDuration);
        _scaleTween.Play();
        if (_image != null)
        {
            _colorTween = _image.DOColor(_originalColor, _holdDuration);
            _colorTween.Play();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            _scaleTween = transform.DOScale(_originalScale * _clickScale, _clickDuration)
                .OnComplete(() => 
                {
                    _scaleTween = transform.DOScale(_originalScale, _clickDuration);
                });
            _scaleTween.Play();
            if (_image != null)
            {
                _colorTween = _image.DOColor(_clickColor, _clickDuration)
                    .OnComplete(() =>
                    {
                        _colorTween = _image.DOColor(_originalColor, _clickDuration);
                    });
                _colorTween.Play();
            }
        }
    }
}

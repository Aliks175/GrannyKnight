using DG.Tweening;
using UnityEngine;

public class UiButton : MonoBehaviour
{
   [SerializeField] private RectTransform _body;
    private Tween _onState;

    private bool _isActiveAnimationbutton => _onState != null && _onState.active;

    private void OnEnable()
    {
        _body.localScale = Vector3.one;
    }

    private void OnDisable()
    {
        DisableState();
    }

    private void OnValidate()
    {
        _body = GetComponent<RectTransform>();
        
    }

    public void OnEnterCursor()
    {
        DisableState();
        _onState = _body.DOScale(1.2f, 0.5f).SetEase(Ease.OutBack);
       
        _onState.SetUpdate(true);
        _onState.Play();
    }

    public void OnExitCursor()
    {
        DisableState();
        _onState = _body.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        _onState.SetUpdate(true);
        _onState.Play();
    }

    public void OnClickCursor()
    {
        _onState = _body.DOShakePosition(0.1f, 10f, 50);
        _onState.SetUpdate(true);
        _onState.Play();
    }

    private void DisableState()
    {
        if (_isActiveAnimationbutton)
        {
            _onState.Kill(true);
        }
    }
}
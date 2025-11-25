using DG.Tweening;
using System;
using UnityEngine;

public class FairyItem : MonoBehaviour
{
    public bool CheckFree => _isFree;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Rigidbody _rigidbody;
    private Tween _tweenProgressPickUp;
    private bool _isFree;
    public event Action<FairyItem> OnLost;

    private void OnDisable()
    {
        _tweenProgressPickUp?.Kill();
    }

    private void Start()
    {
        _isFree = true;
        _tweenProgressPickUp = transform.DOScaleY(4, 1).From(2).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetFairy()
    {
        _isFree = false;
    }

    public void StartProgressPickUp()
    {
        _tweenProgressPickUp.Play();
    }

    public void PickUp()
    {
        // Выключать анимацию , изменять скейл на 1,1,1
        // отключать отображение 
        _tweenProgressPickUp.Pause();
        transform.localScale = Vector3.one;
        _meshRenderer.enabled = false;
    }

    public void LostItem()
    {
        OnLost?.Invoke(this);
    }

    public void DropItem(Vector3 vector3)
    {
        if (!_meshRenderer.enabled)
        {
            _rigidbody.transform.position = vector3;
            _meshRenderer.enabled = true;
        }
        _isFree = true;
        transform.localScale = Vector3.one*2;
        _tweenProgressPickUp.Pause();
    }
}

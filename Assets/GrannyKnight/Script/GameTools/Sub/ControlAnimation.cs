using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ControlAnimation : MonoBehaviour
{
    private Animator _animator;
    private int _idControlisVisible;

    private void Awake()
    {
        _idControlisVisible = Animator.StringToHash(Const.ControlisVisible);
        _animator = GetComponent<Animator>();
    }

    public void ControlShow(bool isVisible)
    {
        _animator.SetBool(_idControlisVisible, isVisible);
    }
}

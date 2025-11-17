using UnityEngine;

public class PlayerControlAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _coefficientSmoothSpeed;
    private float _tempSpeed;
    private bool _tempGround;
    private int _idSpeed;
    private int _idIsGround;

    public void Initialization()
    {
        _idSpeed = Animator.StringToHash("Speed");
        _idIsGround = Animator.StringToHash("IsGround");
    }

    public void SetSpeed(float speed)
    {
        _tempSpeed = Mathf.Lerp(_tempSpeed, speed, _coefficientSmoothSpeed);
        if (!_tempGround) return;
        _animator.SetFloat(_idSpeed, _tempSpeed);
    }

    public void SetCheckGround(bool isGround)
    {
        if (_tempGround == true && isGround == false)
        {
            _animator.SetTrigger(_idIsGround);
        }
        _tempGround = isGround;
    }
}
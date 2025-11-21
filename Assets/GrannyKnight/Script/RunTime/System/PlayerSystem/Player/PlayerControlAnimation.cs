using UnityEngine;

public class PlayerControlAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animatorArmorHand;
    [SerializeField] private Animator _animatorGlovesHand;
    [SerializeField] private float _coefficientSmoothSpeed;
    private Animator _activeAnimator;
    private float _tempSpeed;
    private bool _tempGround;
    private int _idSpeed;
    private int _idIsGround;

    public void Initialization()
    {
        //_idSpeed = Animator.StringToHash("Speed");
        //_idIsGround = Animator.StringToHash("IsGround");
        //_activeAnimator = _animatorArmorHand;
        //ChangeAnimator(_animatorArmorHand);
    }

    public void SetSpeed(float speed)
    {
        _tempSpeed = Mathf.Lerp(_tempSpeed, speed, _coefficientSmoothSpeed);
        if (!_tempGround) return;
        _activeAnimator.SetFloat(_idSpeed, _tempSpeed);
    }

    public void SetCheckGround(bool isGround)
    {
        if (_tempGround == true && isGround == false)
        {
            _activeAnimator.SetTrigger(_idIsGround);
        }
        _tempGround = isGround;
    }

    public void ChangeHand()
    {
        if (_activeAnimator != _animatorArmorHand)
        {
            ChangeAnimator(_animatorArmorHand);
        }
        else
        {
            ChangeAnimator(_animatorGlovesHand);
        }
    }

    private void ChangeAnimator(Animator animator)
    {
        _activeAnimator.gameObject.SetActive(false);
        _activeAnimator = animator;
        _activeAnimator.gameObject.SetActive(true);
    }
}
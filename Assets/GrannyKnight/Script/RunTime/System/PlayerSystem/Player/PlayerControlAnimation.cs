using Unity.Cinemachine;
using UnityEngine;

public class PlayerControlAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animatorArmorHand;
    [SerializeField] private Animator _animatorGlovesHand;
    [SerializeField] private float _coefficientSmoothSpeed;
    [SerializeField] private CinemachineBasicMultiChannelPerlin _virtualCamera;
    [SerializeField] private float _amplitudeGain;
    private Animator _activeAnimator;
    private float _tempSpeed;
    private bool _tempGround;
    private int _idSpeed;
    private int _idIsGround;
    private int _idAir;
    private bool _isArmor;
    private bool _isPlayerControl;

    public void Initialization()
    {
        _idSpeed = Animator.StringToHash("Speed");
        _idIsGround = Animator.StringToHash("IsGround");
        _idAir = Animator.StringToHash("Air");
        _activeAnimator = _animatorArmorHand;
        ChangeAnimator(_animatorArmorHand);
        _isArmor = true;
    }

    public void SetSpeed(float speed)
    {
        _tempSpeed = Mathf.Lerp(_tempSpeed, speed, _coefficientSmoothSpeed);
        if (!_tempGround) return;
        _activeAnimator.SetFloat(_idSpeed, _tempSpeed);
        float temp = (_tempSpeed - 4) / _amplitudeGain;
        _virtualCamera.AmplitudeGain = temp;

        if (!_isPlayerControl) return;
        if (speed > 5)
        {
            SoundSystem.instance.PlayWalk(_isArmor);
        }
    }

    public void ControlMovePlayer(bool isPlayerControl)
    {
        _isPlayerControl = isPlayerControl;
    }

    public void SetCheckGround(bool isGround)
    {
        if (_tempGround == true && isGround == false)
        {
            _activeAnimator.SetTrigger(_idAir);
        }
        _activeAnimator.SetBool(_idIsGround, isGround);
        _tempGround = isGround;


        //if (_tempGround == true && isGround == false)
        //{
        //    _activeAnimator.SetTrigger(_idIsGround);
        //}
        //_tempGround = isGround;
    }

    public void ChangeHand(EquipHand equipHand)
    {
        switch (equipHand)
        {
            case EquipHand.ArmorHand:
                ChangeAnimator(_animatorArmorHand);
                break;
            case EquipHand.GlovesHand:
                ChangeAnimator(_animatorGlovesHand);
                _isArmor = false;
                break;
            default:
                break;
        }
    }

    public void ChangeAnimator(Animator animator)
    {
        _activeAnimator.gameObject.SetActive(false);
        _activeAnimator = animator;
        _activeAnimator.gameObject.SetActive(true);
    }
}

public enum EquipHand
{
    ArmorHand,
    GlovesHand,
    SlingshotHand,
    PodmetatusHand,
    EasterEggsHand,
}
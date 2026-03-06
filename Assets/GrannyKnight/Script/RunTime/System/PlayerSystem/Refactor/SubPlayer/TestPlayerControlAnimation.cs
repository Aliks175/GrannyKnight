using UnityEngine;

namespace Refactor
{
    public class TestPlayerControlAnimation
    {
        private Animator _activeAnimator;
        private float _coefficientSmoothSpeed;
        //private float _amplitudeGain;
        private float _tempSpeed;
        private bool _tempGround;

        private readonly int _idSpeed;
        private readonly int _idIsGround;
        private readonly int _idAir;

        private const string _speed = "Speed";
        private const string _ground = "IsGround";
        private const string _air = "Air";

        public TestPlayerControlAnimation(Animator animator, SettingsPlayer settingsPlayer)
        {
            _activeAnimator = animator;
            _coefficientSmoothSpeed = settingsPlayer.CoefficientSmoothSpeed;
            _idSpeed = Animator.StringToHash(_speed);
            _idIsGround = Animator.StringToHash(_ground);
            _idAir = Animator.StringToHash(_air);
        }

        //public void Initialization()
        //{
        //    //_activeAnimator = _animatorArmorHand;
        //    //ChangeAnimator(_animatorArmorHand);
        //    //_isArmor = true;
        //    //_isPlayerControl = true;
        //}

        public void SetSpeed(float speed)
        {
            _tempSpeed = Mathf.Lerp(_tempSpeed, speed, _coefficientSmoothSpeed);
            if (!_tempGround) return;
            _activeAnimator.SetFloat(_idSpeed, _tempSpeed);

            //float temp = (_tempSpeed - 4) / _amplitudeGain;
            //_virtualCamera.AmplitudeGain = temp;

            //if (!_isPlayerControl) return;
            //if (speed > 5)
            //{
            //    SoundSystem.instance.PlayWalk(_isArmor);
            //}
        }

        public void SetCheckGround(bool isGround)
        {
            if (_tempGround == true && isGround == false)
            {
                _activeAnimator.SetTrigger(_idAir);
            }
            _activeAnimator.SetBool(_idIsGround, isGround);
            _tempGround = isGround;
        }

        public void ChangeAnimator(Animator animator)
        {
            _activeAnimator.gameObject.SetActive(false);
            _activeAnimator = animator;
            _activeAnimator.gameObject.SetActive(true);
        }
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
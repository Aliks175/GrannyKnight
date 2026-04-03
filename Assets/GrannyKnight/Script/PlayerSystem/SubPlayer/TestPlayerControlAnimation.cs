using UnityEngine;

namespace Refactor
{
    public class TestPlayerControlAnimation //: ITickable
    {
        private Animator _activeAnimator;
        private float _coefficientSmoothSpeed;
        private float _tempSpeed;
        private bool _tempGround;
        private int _currenthand;

        #region Readonly
        private readonly int _idSpeed;
        private readonly int _idIsGround;
        private readonly int _idAir;

        private readonly int _TestarmorHand;
        private readonly int _TestglovesHand;

        private readonly int _idSlingWeaponHand;
        private readonly int _idPodmetatusWeaponHand;
        private readonly int _idEasterEggsWeaponHand;

        private const string _speed = "Speed";
        private const string _ground = "IsGround";
        private const string _air = "Air";

        private const string _armorHand = "IdleGray";
        private const string _glovesHand = "IdleGreen";

        private const string _slingWeapon = "IdleSling";
        private const string _podmetatusWeapon = "IdlePodmetatus";
        private const string _easterEggsWeapon = "IdleEasterEggs";

        #endregion

        public TestPlayerControlAnimation(Animator animator, SettingsPlayer settingsPlayer)
        {
            _activeAnimator = animator;
            _coefficientSmoothSpeed = settingsPlayer.CoefficientSmoothSpeed;
            //_idSpeed = Animator.StringToHash(_speed);
            //_idIsGround = Animator.StringToHash(_ground);
            //_idAir = Animator.StringToHash(_air);
            _TestarmorHand = Animator.StringToHash(_armorHand);
            _TestglovesHand = Animator.StringToHash(_glovesHand);
            _idSlingWeaponHand = Animator.StringToHash(_slingWeapon);
            _idPodmetatusWeaponHand = Animator.StringToHash(_podmetatusWeapon);
            _idEasterEggsWeaponHand = Animator.StringToHash(_easterEggsWeapon);
            _currenthand = -1;
        }

        public void ChangeHand(EquipHand equipHand)
        {
            if (_currenthand == (int)equipHand) { return; }
            _currenthand = (int)equipHand;

            Debug.Log($"ChangeHand(EquipHand = {equipHand})");
            switch (equipHand)
            {
                case EquipHand.ArmorHand:
                    _activeAnimator.Play(_TestarmorHand);
                    break;
                case EquipHand.GlovesHand:
                    _activeAnimator.Play(_TestglovesHand);
                    break;
                case EquipHand.SlingshotHand:
                    _activeAnimator.Play(_idSlingWeaponHand);
                    break;
                case EquipHand.PodmetatusHand:
                    _activeAnimator.Play(_idPodmetatusWeaponHand);
                    break;
                case EquipHand.EasterEggsHand:
                    _activeAnimator.Play(_idEasterEggsWeaponHand);
                    break;
            }
        }

        public void SetSpeed(float speed)
        {
            _tempSpeed = Mathf.Lerp(_tempSpeed, speed, _coefficientSmoothSpeed);
            if (!_tempGround) return;
            //_activeAnimator.SetFloat(_idSpeed, _tempSpeed);
        }

        public void SetCheckGround(bool isGround)
        {
            if (_tempGround == true && isGround == false)
            {
                //ChangeHand(EquipHand.ArmorHand);
                //_activeAnimator.SetTrigger(_idAir);
            }
            //_activeAnimator.SetBool(_idIsGround, isGround);
            _tempGround = isGround;
        }
    }
}

public enum EquipHand
{
    none = -1,
    ArmorHand,
    GlovesHand,
    SlingshotHand,
    PodmetatusHand,
    EasterEggsHand,
}
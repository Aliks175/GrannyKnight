using UnityEngine;

namespace Refactor
{
    public class TestPlayerControlAnimation
    {
        private Animator _armorHandAnimator;
        private Animator _glovesHandAnimator;


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

        public TestPlayerControlAnimation(Animator armorHandAnimator, Animator glovesHandAnimator, SettingsPlayer settingsPlayer)
        {
            _armorHandAnimator = armorHandAnimator;
            _glovesHandAnimator = glovesHandAnimator;

            _coefficientSmoothSpeed = settingsPlayer.CoefficientSmoothSpeed;
            _idSpeed = Animator.StringToHash(_speed);
            _idIsGround = Animator.StringToHash(_ground);
            _idAir = Animator.StringToHash(_air);
            _TestarmorHand = Animator.StringToHash(_armorHand);
            _TestglovesHand = Animator.StringToHash(_glovesHand);
            _idSlingWeaponHand = Animator.StringToHash(_slingWeapon);
            _idPodmetatusWeaponHand = Animator.StringToHash(_podmetatusWeapon);
            _idEasterEggsWeaponHand = Animator.StringToHash(_easterEggsWeapon);
            _currenthand = -1;
        }

        public void ChangeHand(OnDataEquipHand equipHand)
        {
            if (_currenthand == (int)equipHand.EquipHand) { return; }
            _currenthand = (int)equipHand.EquipHand;

            if (CheckClearHand(equipHand.EquipHand))
            {
                if (equipHand.EquipHand == EquipHand.ArmorHand)
                {
                    ChangeHand(_armorHandAnimator);
                }
                else
                {
                    ChangeHand(_glovesHandAnimator);
                }
                return;
            }
            ChangeHand(equipHand.Weapon.Point.AnimatorHand);
        }

        public void SetSpeed(float speed)
        {
            if (_activeAnimator == null) { return; }
            _tempSpeed = Mathf.Lerp(_tempSpeed, speed, _coefficientSmoothSpeed);
            if (!_tempGround) return;
            _activeAnimator.SetFloat(_idSpeed, _tempSpeed);
        }

        public void SetCheckGround(bool isGround)
        {
            if (_activeAnimator == null) { return; }
            if (_tempGround == true && isGround == false)
            {
                _activeAnimator.SetTrigger(_idAir);
            }
            _activeAnimator.SetBool(_idIsGround, isGround);
            _tempGround = isGround;
        }

        private bool CheckClearHand(EquipHand equipHand)
        {
            return equipHand == EquipHand.GlovesHand || equipHand == EquipHand.ArmorHand;
        }

        private void ChangeHand(Animator newAnimator)
        {
            if (newAnimator == null) return;
            if (_activeAnimator != null)
            {
                _activeAnimator.gameObject.SetActive(false);
            }
            _activeAnimator = newAnimator;
            _activeAnimator.gameObject.SetActive(true);
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
    SwordHand
}
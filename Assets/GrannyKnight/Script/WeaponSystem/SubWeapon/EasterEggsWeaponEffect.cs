using UnityEngine;
using UnityEngine.Events;

public class EasterEggsWeaponEffect : WeaponEffectAbstract
{
    public override Animator AnimatorWeapon => throw new System.NotImplementedException();

    public override int IdWeapon => throw new System.NotImplementedException();

    private Animator _animator;
    private int _shootAnimationID;
    private int _endShootAnimationID;
    private int _isShootAnimationID;

    public UnityEvent OnShooteble;
    public UnityEvent OnChargeeble;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _shootAnimationID = Animator.StringToHash("Shoot");
        _endShootAnimationID = Animator.StringToHash("EndShoot");
        _isShootAnimationID = Animator.StringToHash("IsShoot");
    }

    public override void DisableSound()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttackOne()
    {
        _animator.SetBool(_isShootAnimationID, false);
        OnShooteble?.Invoke();
    }

    public override void OnCharge()
    {
        _animator.SetBool(_isShootAnimationID, true);
        _animator.SetTrigger(_shootAnimationID);
        OnChargeeble?.Invoke();
    }
}

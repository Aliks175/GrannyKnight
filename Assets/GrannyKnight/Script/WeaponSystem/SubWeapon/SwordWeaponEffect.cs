using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class SwordWeaponEffect : WeaponEffectAbstract
{
    public override Animator AnimatorWeapon => _animator;
    public override int IdWeapon => _idWeapon;
    [SerializeField] private int _idWeapon;

    private Animator _animator;
    private int _shootAnimationID;
    private int _hitAnimationID;
    private int _blockAnimationID;
    //private int _endShootAnimationID;
    //private int _isShootAnimationID;

    public UnityEvent OnShooteble;
    public UnityEvent OnEndShooteble;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _shootAnimationID = Animator.StringToHash("Shoot");
        _hitAnimationID = Animator.StringToHash("Hit");
        _blockAnimationID = Animator.StringToHash("IsBlock");
        //_endShootAnimationID = Animator.StringToHash("EndShoot");
        //_isShootAnimationID = Animator.StringToHash("IsShoot");
    }

    public override void DisableSound()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnAttackOne()
    {
        Debug.Log("OnAttackOne");
        //_animator.SetBool(_isShootAnimationID, true);
        _animator.SetTrigger(_hitAnimationID);
        //OnShooteble?.Invoke();
    }

    public override void OnAttackTwo()
    {
        Debug.Log("OnAttackTwo");
        _animator.SetTrigger(_shootAnimationID);
    }

    public override void OnBlock(bool isActive)
    {
        Debug.Log($"OnBlock = {isActive}");
        _animator.SetBool(_blockAnimationID, isActive);
    }

    //public override void OnEndShoot()
    //{
    //    _animator.SetBool(_isShootAnimationID, false);
    //    _animator.SetTrigger(_endShootAnimationID);
    //    OnEndShooteble?.Invoke();
    //}
}

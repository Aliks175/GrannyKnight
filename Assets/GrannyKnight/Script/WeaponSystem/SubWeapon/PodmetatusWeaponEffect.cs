using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Animator))]
public class PodmetatusWeaponEffect : WeaponEffectAbstract
{
    public override Animator AnimatorWeapon => throw new System.NotImplementedException();
  
    public override int IdWeapon => throw new System.NotImplementedException();
    [SerializeField] private ParticleSystem _flash;
    [SerializeField] private ParticleSystem _lazerBullet;

    private Animator _animator;
    private int _shootAnimationID;
    private int _endShootAnimationID;
    private int _isShootAnimationID;

    public UnityEvent OnShooteble;
    public UnityEvent OnEndShooteble;

    private void Awake()
    {
        _animator = GetComponent<Animator> ();
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
        _animator.SetBool(_isShootAnimationID, true);
        _animator.SetTrigger(_shootAnimationID);
        OnShooteble?.Invoke ();
    }

    public override void OnEndShoot()
    {
        _animator.SetBool(_isShootAnimationID, false);
        _animator.SetTrigger(_endShootAnimationID);
        OnEndShooteble?.Invoke();
    }
}

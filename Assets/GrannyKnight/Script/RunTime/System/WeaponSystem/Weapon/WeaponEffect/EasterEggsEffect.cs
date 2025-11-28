using UnityEngine;
using UnityEngine.Events;

public class EasterEggsEffect : WeaponEffectAbstract
{
    public override Animator AnimatorWeapon => _animator;
    public override int IdWeapon => _idWeapon;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _idWeapon;
    private IFireble _testWeapon;
    private int _shootAnimationID;
    private int _endShootAnimationID;
    private int _isShootAnimationID;
    private bool _isShootNow;

    public UnityEvent OnPreFire;
    public UnityEvent OnFire;
    public UnityEvent OnEndFire;
    public UnityEvent OnSystemDisableSound;

    private void OnDisable()
    {
        if (_testWeapon != null)
        {
            _testWeapon.OnFireRaycast -= Fire;
            _testWeapon.OnEndFire -= EndFire;
            _testWeapon.OnPreFire -= PreFire;
        }
    }

    public override void Initialization(IFireble testWeapon)
    {
        _testWeapon = testWeapon;
        _shootAnimationID = Animator.StringToHash("Shoot");
        _endShootAnimationID = Animator.StringToHash("EndShoot");
        _isShootAnimationID = Animator.StringToHash("IsShoot");
        _testWeapon.OnPreFire += PreFire;
        _testWeapon.OnFireRaycast += Fire;
        _testWeapon.OnEndFire += EndFire;
    }

    private void PreFire()
    {
       
        if (_isShootNow) return;
        //_animator.SetBool(_isShootAnimationID, true);
        _animator.SetTrigger(_shootAnimationID);
        _isShootNow = true;
    }

    private void Fire(RaycastHit raycastHit)
    {
        if (_animator != null)
        {
            //_animator.SetBool(_isShootAnimationID, false);
            //_animator.SetTrigger(_shootAnimationID);
        }

        if (_audioSource != null)
        {
            _audioSource.Play();
        }

        //if (_particleSystem != null)
        //{
        //    _particleSystem.Play();
        //}
        
    }

    private void PlayShootEffect()
    {
        OnPreFire?.Invoke();
        OnFire?.Invoke();
        if (_particleSystem != null)
        {
            _particleSystem.Play();
        }
    }

    private void EndFire()
    {
        if (_animator != null)
        {
            _animator.SetBool(_isShootAnimationID, false);
            _animator.SetTrigger(_endShootAnimationID);
            if (_particleSystem != null)
            {
                _particleSystem.Stop();
            }
        }
        OnEndFire?.Invoke();
        _isShootNow = false;
    }

    public override void DisableSound()
    {
        OnSystemDisableSound?.Invoke();
    }
}
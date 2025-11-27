using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class PhysicsEffect : WeaponEffectAbstract
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
    public UnityEvent OnFire;
    public UnityEvent OnEndFire;
    public UnityEvent OnSystemDisableSound;

    private void OnDisable()
    {
        if (_testWeapon != null)
        {
            _testWeapon.OnFirePhysics -= Fire;
            _testWeapon.OnEndFire -= ControlFire;
        }
    }

    public override void DisableSound()
    {
        OnSystemDisableSound?.Invoke();
    }

    public override void Initialization(IFireble testWeapon)
    {
        _testWeapon = testWeapon;
        _shootAnimationID = Animator.StringToHash("Shoot");
        _endShootAnimationID = Animator.StringToHash("EndShoot");
        _isShootAnimationID = Animator.StringToHash("IsShoot");
        _testWeapon.OnFirePhysics += Fire;
        _testWeapon.OnEndFire += ControlFire;
    }

    private void Fire()
    {
        if (_animator != null)
        {
            _animator.SetBool(_isShootAnimationID, true);
            _animator.SetTrigger(_shootAnimationID);
        }

        if (_audioSource != null)
        {
            _audioSource.Play();
        }

        if (_particleSystem != null)
        {
            _particleSystem.Play();
        }
        OnFire?.Invoke();
        //CreateMark(typeShoot.raycastHit);
    }

    //private void CreateMark(RaycastHit Pos)
    //{
    //    if (Pos.point != Vector3.zero)
    //    {
    //        Mark tempMark = _controlViewMark.GetMark();
    //        tempMark.transform.position = Pos.point;
    //        tempMark.transform.rotation = Quaternion.LookRotation(Pos.normal);
    //        tempMark.SetMark();
    //    }
    //}

    private void ControlFire()
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
    }
}

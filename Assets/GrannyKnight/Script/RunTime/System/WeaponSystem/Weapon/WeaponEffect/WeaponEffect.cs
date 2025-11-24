using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class WeaponEffect : WeaponEffectAbstract
{
    public override Animator AnimatorWeapon => _animator;
    public override int IdWeapon => _idWeapon;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _idWeapon;
    //private ControlViewMark _controlViewMark;
    private IFireble _testWeapon;
    private int _shootAnimationID;
    private int _endShootAnimationID;
    private int _isShootAnimationID;

    private bool _isShootNow;

    public UnityEvent OnFire;
    public UnityEvent OnEndFire;

    private void OnDisable()
    {
        if (_testWeapon != null)
        {
            _testWeapon.OnFire -= Fire;
            _testWeapon.OnEndFire -= ControlFire;
        }
    }

    public override void Initialization(IFireble testWeapon, ControlViewMark controlViewMark)
    {
        _testWeapon = testWeapon;
        //_controlViewMark = controlViewMark;
        _shootAnimationID = Animator.StringToHash("Shoot");
        _endShootAnimationID = Animator.StringToHash("EndShoot");
        _isShootAnimationID = Animator.StringToHash("IsShoot");
        _testWeapon.OnStartFire += PreFire;
        _testWeapon.OnFire += Fire;
        _testWeapon.OnEndFire += ControlFire;
    }

    private void PreFire()
    {
        _animator.SetBool(_isShootAnimationID, true);
        _animator.SetTrigger(_shootAnimationID);
        //_isShootNow = true;
    }

    private void Fire(TypeShoot typeShoot)
    {
        //if (_animator != null)
        //{
        //    if (typeShoot.IsShootAutoFire && _isShootNow) return;
        //    _animator.SetTrigger(_shootAnimationID);
        //}

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
        _isShootNow = false;
    }
}
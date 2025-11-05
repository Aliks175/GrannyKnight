using UnityEngine;

[SelectionBase]
public class WeaponEffect : MonoBehaviour
{
    public int IdWeapon => _idWeapon;
    [SerializeField] private Transform _startFire;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _shootMark;
    [SerializeField] private int _idWeapon;
    private ControlViewMark _controlViewMark;
    private ShootingWeapon _testWeapon;
    private int _shootAnimationID;
    private int _endShootAnimationID;
    private int _isShootAnimationID;

    private void OnDisable()
    {
        if (_testWeapon != null)
        {
            _testWeapon.OnFire -= Fire;
            _testWeapon.OnEndFire -= ControlFire;
        }
    }

    public void Initialization(ShootingWeapon testWeapon, ControlViewMark controlViewMark)
    {
        _testWeapon = testWeapon;
        _controlViewMark = controlViewMark;
        _shootAnimationID = Animator.StringToHash("Shoot");
        _endShootAnimationID = Animator.StringToHash("EndShoot");
        _isShootAnimationID = Animator.StringToHash("IsShoot");
        _testWeapon.OnFire += Fire;
        _testWeapon.OnEndFire += ControlFire;
    }

    private void Fire(TypeShoot typeShoot)
    {
        _animator.SetBool(_isShootAnimationID, true);
        _animator.SetTrigger(_shootAnimationID);
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
        if (_particleSystem != null)
        {
            _particleSystem.Play();
        }
        CreateMark(typeShoot.raycastHit);
    }

    private void CreateMark(RaycastHit Pos)
    {
        if (_shootMark != null && Pos.point != Vector3.zero)
        {
            Mark tempMark = _controlViewMark.GetMark();
            tempMark.transform.position = Pos.point;
            tempMark.transform.rotation = Quaternion.LookRotation(Pos.normal);
            tempMark.SetMark();
        }
    }

    private void ControlFire()
    {
        _animator.SetBool(_isShootAnimationID, false);
        _animator.SetTrigger(_endShootAnimationID);
        if (_particleSystem != null)
        {
            _particleSystem.Stop();
        }
    }
}
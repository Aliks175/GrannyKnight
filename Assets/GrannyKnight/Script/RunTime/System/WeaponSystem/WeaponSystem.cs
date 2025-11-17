using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private ChooseWeaponView _chooseWeaponView;
    [SerializeField] private ShootingWeapon _shootingWeapon;
    [SerializeField] private PhysicsWeapon _physicsWeapon;
    [SerializeField] private ControlViewMark _controlViewMark;
    private IPlayerDatable _playerData;
    private Weapon _activeWeapon;

    private void OnDisable()
    {
        if (_playerData?.ChooseWeapon == null) return;
        
        _playerData.ChooseWeapon.OnChangeWeapon -= _chooseWeaponView.ChangeWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon -= _shootingWeapon.SetWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon -= SetWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon -= Contect => _shootingWeapon.StopFire();
        _playerData.ChooseWeapon.OnChangeWeapon -= _physicsWeapon.SetWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon -= Contect => _physicsWeapon.ResetShot();
    }

    public void Initialization(IPlayerDatable playerDatable, Camera camera)
    {
        _playerData = playerDatable;
        _chooseWeaponView.Initialization();
        _shootingWeapon.Initialization(camera, _controlViewMark);
        _physicsWeapon.Initialization(camera); 
        _controlViewMark.Initialization();
        SetUp();
    }

    private void SetUp()
    {
        _playerData.ChooseWeapon.OnChangeWeapon += _chooseWeaponView.ChangeWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon += _shootingWeapon.SetWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon += SetWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon += Contect => _shootingWeapon.StopFire();
        _chooseWeaponView.OnWeaponEquip += _shootingWeapon.SetWeaponEffect;
         _playerData.ChooseWeapon.OnChangeWeapon += _physicsWeapon.SetWeapon;
        //_chooseWeaponView.OnWeaponEquip -= _shootingWeapon.SetWeaponEffect;
        _playerData.ChooseWeapon.OnChangeWeapon += Contect => _physicsWeapon.ResetShot();
    }

    public void Shoot(InputAction.CallbackContext value)
    {
        if (_activeWeapon == null) return; 
        if (_activeWeapon.TypeShoot == TypeShootPhysics.Raycast) _shootingWeapon.Shoot(value);
        else if (_activeWeapon.TypeShoot == TypeShootPhysics.Physics) _physicsWeapon.Shoot(value);
    }
    private void SetWeapon(Weapon weapon)
    {
        _activeWeapon = weapon;
    }
}

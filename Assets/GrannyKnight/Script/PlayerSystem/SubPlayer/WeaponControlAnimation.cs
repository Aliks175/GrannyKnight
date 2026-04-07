using UnityEngine;

public class WeaponControlAnimation
{
    private WeaponEffectAbstract _weaponEffect;
    private TestWeapon _testWeapon;
    private bool _isEquipedWeapon => _testWeapon != null;

    //public WeaponControlAnimation()
    //{

    //}

    public void SetWeapon(TestWeapon testWeapon)
    {
        Debug.Log($"SetWeapon = null {testWeapon == null}");
        if (_testWeapon == testWeapon) { return; }
        if (!CheckWeapon(testWeapon)) { return; }
    }

    public void OnShoot()
    {
        Debug.Log($"WeaponControlAnimation --- OnShoot ");
        if (_isEquipedWeapon)
        {
            _weaponEffect.OnShoot();
            Debug.Log($"OnShoot = null {_isEquipedWeapon}");

        }
    }

    public void OnEndShoot()
    {
        if (_isEquipedWeapon)
        {
            _weaponEffect.OnEndShoot();
            Debug.Log($"OnEndShoot = null {_isEquipedWeapon}");

        }
    }

    public void OnCharge()
    {
        if (_isEquipedWeapon)
        {
            _weaponEffect.OnCharge();

            Debug.Log($"OnCharge = null {_isEquipedWeapon}");
        }
    }

    private bool CheckWeapon(TestWeapon testWeapon)
    {
        bool isFindWeaponEffect = false;
        _testWeapon = testWeapon;
        if (testWeapon == null)
        {
            Debug.Log($"CheckWeapon = null ");
            _weaponEffect = null;
        }
        else
        {
            _weaponEffect = testWeapon.Point.WeaponEffectAbstract;
            isFindWeaponEffect = true;
            Debug.Log($"CheckWeapon = NOT null");
        }
        return isFindWeaponEffect;
    }
}
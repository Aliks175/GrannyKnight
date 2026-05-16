using Refactor;
using System;
using Zenject;

public class ImporterShootingControlAnimation : IDisposable, IInitializable
{
    private ShootingRaycast _shootingRaycast;
    private ShootingPhysics _shootingPhysics;
    private HitingMelee _hitingMelee;
    private WeaponControlAnimation _weaponControlAnimation;

    public ImporterShootingControlAnimation(ShootingRaycast shootingRaycast, ShootingPhysics shootingPhysics, HitingMelee hitingMelee, WeaponControlAnimation weaponControlAnimation)
    {
        _shootingRaycast = shootingRaycast;
        _shootingPhysics = shootingPhysics;
        _hitingMelee = hitingMelee;
        _weaponControlAnimation = weaponControlAnimation;
    }

    public void Dispose()
    {
        _shootingRaycast.OnShoot -= OnShoot;
        _shootingRaycast.OnEndShoot -= OnEndShoot;
        _shootingPhysics.OnShoot -= OnShoot;
        _shootingPhysics.OnCharge -= OnCharge;
        _hitingMelee.OnAttackOne -= OnShoot;
        _hitingMelee.OnAttackTwo -= OnShootTwo;
        _hitingMelee.OnBlock -= OnBlock;
    }

    public void Initialize()
    {
        _shootingRaycast.OnShoot += OnShoot;
        _shootingRaycast.OnEndShoot += OnEndShoot;
        _shootingPhysics.OnShoot += OnShoot;
        _shootingPhysics.OnCharge += OnCharge;

        _hitingMelee.OnAttackOne += OnShoot;
        _hitingMelee.OnAttackTwo += OnShootTwo;
        _hitingMelee.OnBlock += OnBlock;
    }
    private void OnShoot()
    {
        _weaponControlAnimation.OnShoot();
    }

    private void OnShootTwo()
    {
        _weaponControlAnimation.OnShootTwo();
    }

    private void OnBlock(bool isActive)
    {
        _weaponControlAnimation.OnBlock(isActive);
    }

    private void OnCharge()
    {
        _weaponControlAnimation.OnCharge();
    }


    private void OnEndShoot()
    {
        _weaponControlAnimation.OnEndShoot();
    }
}


using Refactor;
using System;
using UnityEngine;
using Zenject;

public class ImporterShootingControlAnimation : IDisposable, IInitializable
{
    private ShootingRaycast _shootingRaycast;
    private ShootingPhysics _shootingPhysics;
    private WeaponControlAnimation _weaponControlAnimation;

    public ImporterShootingControlAnimation(ShootingRaycast shootingRaycast, ShootingPhysics shootingPhysics, WeaponControlAnimation weaponControlAnimation)
    {
        _shootingRaycast = shootingRaycast;
        _shootingPhysics = shootingPhysics;
        _weaponControlAnimation = weaponControlAnimation;
    }

    public void Dispose()
    {
        _shootingRaycast.OnShoot -= OnShoot;
        _shootingRaycast.OnEndShoot -= OnEndShoot;
        _shootingPhysics.OnShoot -= OnShoot;
        _shootingPhysics.OnCharge -= OnCharge;
    }

    public void Initialize()
    {
        _shootingRaycast.OnShoot += OnShoot;
        _shootingRaycast.OnEndShoot += OnEndShoot;
        _shootingPhysics.OnShoot += OnShoot;
        _shootingPhysics.OnCharge += OnCharge;
    }

    private void OnCharge()
    {
        _weaponControlAnimation.OnCharge();
    }

    private void OnShoot()
    {
        _weaponControlAnimation.OnShoot();
    }

    private void OnEndShoot()
    {
        _weaponControlAnimation.OnEndShoot();
    }
}


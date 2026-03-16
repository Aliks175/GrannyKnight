using Refactor;
using System;
using Zenject;

public class ImporterPlayerWeaponAnimation : IDisposable, IInitializable
{
    private TestPlayerWeapon _playerWeapon;
    private TestPlayerControlAnimation _playerControlAnimation;

    public ImporterPlayerWeaponAnimation(TestPlayerWeapon playerWeapon, TestPlayerControlAnimation playerControlAnimation)
    {
        _playerWeapon = playerWeapon;
        _playerControlAnimation = playerControlAnimation;
    }

    public void Dispose()
    {
        _playerWeapon.OnChangeHand -= OnChangeWeapon;
    }

    public void Initialize()
    {
        _playerWeapon.OnChangeHand += OnChangeWeapon;
    }

    private void OnChangeWeapon(object sender, OnDataEquipHand dataEquipHand)
    {
        _playerControlAnimation.ChangeHand(dataEquipHand.EquipHand);
    }
}
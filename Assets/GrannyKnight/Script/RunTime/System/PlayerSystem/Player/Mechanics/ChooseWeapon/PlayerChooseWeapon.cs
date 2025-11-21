using System;
using UnityEngine;

public class PlayerChooseWeapon : MonoBehaviour
{
    [SerializeField] private BazeWeapon _slingshot;
    [SerializeField] private BazeWeapon _podmetatus;
    [SerializeField] private BazeWeapon _easterEggs;
    private Weapon _slingshotHand;
    private Weapon _podmetatusHand;
    private Weapon _easterEggsHand;
    private Weapon _equipWeapon;
    private IPlayerDatable _iplayerDatable;
    public event Action<Weapon> OnChangeWeapon;
    public event Action<EquipHand> OnClearHand;

    public void Initialization(IPlayerDatable playerDatable )
    {
        _iplayerDatable = playerDatable;
        CreateWeapon(ref _slingshotHand, _slingshot);
        CreateWeapon(ref _podmetatusHand, _podmetatus);
        CreateWeapon(ref _easterEggsHand, _easterEggs);
    }

    public void GiveWeapon(EquipHand slotNumber)
    {
        switch (slotNumber)
        {
            case EquipHand.ArmorHand:
            case EquipHand.GlovesHand:
                OnClearHand?.Invoke(slotNumber);
                _equipWeapon = null;
                OnChangeWeapon?.Invoke(_equipWeapon);
                break;
            case EquipHand.SlingshotHand:
                EquipWeapon(ref _slingshotHand);
                break;
            case EquipHand.PodmetatusHand:
                EquipWeapon(ref _podmetatusHand);
                break;
            case EquipHand.EasterEggsHand:
                EquipWeapon(ref _easterEggsHand);
                break;
            default:
                break;
        }
    }

    private void EquipWeapon(ref Weapon tempWeapon)
    {
        if (tempWeapon != null)
        {
            _equipWeapon = tempWeapon;
            OnChangeWeapon?.Invoke(_equipWeapon);
        }
    }

    private void CreateWeapon(ref Weapon weapon, BazeWeapon bazeWeapon)
    {
        weapon = new();
        weapon.Initialization(bazeWeapon, _iplayerDatable);
    }
}
using Refactor;
using UnityEngine;
using Zenject;

public class GetterWeapon : MonoBehaviour
{
    [SerializeField] private EquipHand _equipHand;
    private PlayerCharacter _playerWeapon;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
        Debug.Log("GetterWeapon Construct ");
        
    }

    public void Active()
    {
        CheckPlayer();
        _playerWeapon.GiveWeapon(_equipHand);
    }

    private void CheckPlayer()
    {
        if (_playerWeapon == null)
        {
            _playerWeapon = _systemBuss.GetPlayer();
        }
        if (_playerWeapon == null)
        {
            Debug.LogError("Not Found Player");
        }
    }
}

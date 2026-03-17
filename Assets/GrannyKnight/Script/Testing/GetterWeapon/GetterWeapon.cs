using UnityEngine;

public class GetterWeapon : MonoBehaviour
{
    [SerializeField] private EquipHand _equipHand;
    [SerializeField] private PlayerChooseWeapon _playerWeapon;

    private void Start()
    {
        if(_playerWeapon == null)
        {
            _playerWeapon = GameObject.FindFirstObjectByType<PlayerChooseWeapon>();
        }
    }

    public void Active()
    {
        _playerWeapon.DisableSoundWeapon();
        _playerWeapon.GiveWeapon(_equipHand);
    }
}

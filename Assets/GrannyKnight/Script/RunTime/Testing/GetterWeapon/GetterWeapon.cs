using UnityEngine;

public class GetterWeapon : MonoBehaviour
{
    [SerializeField] private EquipHand _equipHand;
    [SerializeField] private PlayerChooseWeapon _playerWeapon;

    public void Active()
    {
        _playerWeapon.GiveWeapon(_equipHand);
    }
}

using Cysharp.Threading.Tasks;
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
    }

    public void Active()
    {
        WaitPlayer().Forget();
    }

    private async UniTaskVoid WaitPlayer()
    {
        if (CheckPlayer())
        {
            PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
            _playerWeapon = playerCharacter;
        }
        if (CheckPlayer()) { return; }
        _playerWeapon.GiveWeapon(_equipHand);
    }

    private bool CheckPlayer()
    {
        return _playerWeapon == null;
    }
}
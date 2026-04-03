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
        //Debug.Log($"GetterWeapon Construct - {gameObject.name}");
    }

    //private void OnEnable()
    //{
    //    Debug.Log($"SystemBuss - null {_systemBuss == null}");
    //    if (_systemBuss == null) { return; }
    //    WaitPlayer().Forget();
    //}

    public void Active()
    {
        //WaitPlayer().Forget();
        //CheckPlayer();
        Debug.Log($"_playerWeapon - null {_playerWeapon == null}");
        WaitPlayer().Forget();
        
    }

    //private void CheckPlayer()
    //{
    //    if (_playerWeapon == null)
    //    {
    //        _playerWeapon = _systemBuss.GetPlayer();
    //    }
    //    if (_playerWeapon == null)
    //    {
    //        Debug.LogError("Not Found Player");
    //    }
    //}

    private async UniTaskVoid WaitPlayer()
    {
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        _playerWeapon = playerCharacter;
        _playerWeapon.GiveWeapon(_equipHand);
        Debug.Log($"_playerWeapon - null {_playerWeapon == null}");
        //SetPlayer(playerCharacter);
    }
}

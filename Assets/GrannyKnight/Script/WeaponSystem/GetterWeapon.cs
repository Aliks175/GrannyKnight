using Cysharp.Threading.Tasks;
using Refactor;
using UnityEngine;
using Zenject;
using static UnityEditor.Experimental.GraphView.GraphView;

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

    private void OnEnable()
    {
        if (_systemBuss == null) { return; }
        WaitPlayer().Forget();
    }

    public void Active()
    {
        //WaitPlayer().Forget();
        //CheckPlayer();
        _playerWeapon.GiveWeapon(_equipHand);
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
        //SetPlayer(playerCharacter);
    }
}

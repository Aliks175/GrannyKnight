using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Teleported : MonoBehaviour
{
    [SerializeField] private Transform _positionTeleported;
    private PlayerCharacter _player;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    private void OnEnable()
    {
        WaitPlayer().Forget();
    }

    public void ActiveTeleport()
    {
        //if (CheckPlayer()) { return; }
        _player.CharacterController.enabled = false;
        //_player.gameObject.SetActive(false);
        _player.transform.position = _positionTeleported.position;
        _player.CharacterController.enabled = true;
        //_player.gameObject.SetActive(true);
    }

    private async UniTaskVoid WaitPlayer()
    {
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        _player = playerCharacter;
        //SetPlayer(playerCharacter);
    }


    //private bool CheckPlayer()
    //{
    //    if (_player == null) 
    //    {
    //        _player = _systemBuss.GetPlayer();
    //    }

    //    return _player == null;
    //}
}
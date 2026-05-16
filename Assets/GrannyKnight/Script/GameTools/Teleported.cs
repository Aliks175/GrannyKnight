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

    public void ActiveTeleport()
    {
        WaitPlayer().Forget();
    }

    private async UniTaskVoid WaitPlayer()
    {
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        if (playerCharacter == null) { return; }
        _player = playerCharacter;
        OnTeleported();
    }

    private void OnTeleported()
    {
        _player.CharacterController.enabled = false;
        _player.transform.position = _positionTeleported.position;
        _player.transform.forward = _positionTeleported.forward;
        //Debug.Log($"{gameObject.name}_positionTeleported = {_positionTeleported.position}");
        _player.CharacterController.enabled = true;
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DisablePlayer : MonoBehaviour
{
    private PlayerCharacter _player;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    public void OnActivePlayer(bool isActive)
    {
        if (_player == null)
        {
            WaitPlayer(isActive).Forget();
        }
        else
        {
            _player.gameObject.SetActive(isActive);
        }
    }

    private async UniTaskVoid WaitPlayer(bool isActive)
    {
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        if (playerCharacter == null) { return; }
        _player = playerCharacter;
        _player.gameObject.SetActive(isActive);
    }
}
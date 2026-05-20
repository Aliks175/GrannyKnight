using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class LessenPlayer : MonoBehaviour
{
    [SerializeField] private float _sizePlayer;
    private PlayerCharacter _player;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    public void ActiveLessen()
    {
        WaitPlayer().Forget();
    }

    private async UniTaskVoid WaitPlayer()
    {
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        if (playerCharacter == null) { return; }
        _player = playerCharacter;
        OnLessen();
    }

    private void OnLessen()
    {
        _player.ChangeSize(_sizePlayer);
    }
}
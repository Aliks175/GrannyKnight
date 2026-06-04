using Cysharp.Threading.Tasks;
using System;

public class SystemBuss
{
    private PlayerCharacter _player;
    private GameUi _gameUi;

    public event Action<PlayerUi> OnConstructPlayerUi;
    public event Action OnReadySpawnPlayer;
    public event Action OnEndDialog;
    public event Action<PlayerCharacter> OnSpawnPlayer;
    public event Action<IEventHistoryble> OnEventHistory;

    public void ConstructPlayerUi(PlayerUi playerUi)
    {
        OnConstructPlayerUi?.Invoke(playerUi);
    }

    public void ConstructGameUi(GameUi gameUi)
    {
        _gameUi = gameUi;
    }

    public void Pause()
    {
        _gameUi.OnPause();
    }

    public void ReadySpawnPlayer( )
    {
        OnReadySpawnPlayer?.Invoke();
    }

    public void SpawnPlayer(PlayerCharacter playerCharacter)
    {
        _player = playerCharacter;
        OnSpawnPlayer?.Invoke(playerCharacter);
    }

    public async UniTask<PlayerCharacter> GetPlayer()
    {
        await UniTask.WaitUntil(() => _player != null);
        return _player;
    }

    public void SetEventHistory(IEventHistoryble interactebleEventHistory)
    {
        OnEventHistory?.Invoke(interactebleEventHistory);
    }

    public void EndDialog()
    {
        OnEndDialog?.Invoke();
    }
}
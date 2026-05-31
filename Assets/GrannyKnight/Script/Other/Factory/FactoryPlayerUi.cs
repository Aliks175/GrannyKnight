using UnityEngine;
using Zenject;

public class FactoryPlayerUi
{
    private PlayerUi _playerUi;
    private GameUi _gameUi;
    private DiContainer _diContainer;

    public FactoryPlayerUi(PlayerUi playerUi, GameUi gameUi, DiContainer diContainer)
    {
        _playerUi = playerUi;
        _diContainer = diContainer;
        _gameUi = gameUi;
    }

    public PlayerUi CreatePlayerUi()
    {
        return _diContainer.InstantiatePrefabForComponent<PlayerUi>(_playerUi, Vector3.zero, Quaternion.identity, null);
    }

    public GameUi CreateGameUi()
    {
        return _diContainer.InstantiatePrefabForComponent<GameUi>(_gameUi, Vector3.zero, Quaternion.identity, null);
    }
}
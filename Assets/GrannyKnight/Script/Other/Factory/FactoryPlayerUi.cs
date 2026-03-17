using UnityEngine;
using Zenject;

public class FactoryPlayerUi
{
    private PlayerUi _playerUi;
    private DiContainer _diContainer;

    public FactoryPlayerUi(PlayerUi playerUi, DiContainer diContainer)
    {
        _playerUi = playerUi;
        _diContainer = diContainer;
    }

    public PlayerUi CreatePlayerUi()
    {
        return _diContainer.InstantiatePrefabForComponent<PlayerUi>(_playerUi, Vector3.zero, Quaternion.identity, null);
    }
}
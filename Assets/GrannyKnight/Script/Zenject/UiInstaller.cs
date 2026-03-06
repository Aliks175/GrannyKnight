using Refactor;
using UnityEngine;
using Zenject;

public class UiInstaller : MonoInstaller
{
    [SerializeField] private PlayerUi _playerUi;

    public override void InstallBindings()
    {
        BindUi();
    }

    private void BindUi()
    {
        Container.Bind<PlayerUi>()
       .FromInstance(_playerUi)
       .AsSingle();
    }
}
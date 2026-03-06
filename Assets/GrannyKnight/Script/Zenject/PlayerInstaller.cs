using Refactor;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;


public class PlayerInstaller : MonoInstaller
{

    [SerializeField] private TypePlay _typePlay;
    [Header("Continue")]
    [SerializeField] private TestPlayerCharacter _playerCharecter;
    [SerializeField] private SettingsPlayer _settingsPlayer;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _groundPoint;

    [Header("PlayerLook/PlayerInteracteble")]
    [SerializeField] private Transform _headSlot;

    [Header("PlayerAim")]
    [SerializeField] private CinemachineCamera cinemachineCamera;

    [Header("PlayerControlAnimation")]
    [SerializeField] private Animator _animatorHand;


    public override void InstallBindings()
    {
        BindPlayer();
        BindInput();
        BindImporter();
        if (_typePlay == TypePlay.Game)
        {
            BindImporterUI();
        }
    }

    private void BindPlayer()
    {
        Container.Bind<TestPlayerCharacter>()
        .FromInstance(_playerCharecter)
        .AsSingle();

        Container.BindInterfacesAndSelfTo<TestPlayerMove>()
            .AsSingle()
            .WithArguments(_settingsPlayer, _characterController, _groundPoint)
            .NonLazy();

        Container.Bind<TestPlayerLook>()
            .AsSingle()
            .WithArguments(_settingsPlayer, _headSlot, _playerCharecter.transform)
            .NonLazy();

        Container.BindInterfacesAndSelfTo<TestPlayerAim>()
            .AsSingle()
            .WithArguments(_settingsPlayer, cinemachineCamera)
            .NonLazy();

        Container.BindInterfacesAndSelfTo<TestPlayerInteracteble>()
            .AsSingle()
            .WithArguments(_settingsPlayer, _headSlot)
            .NonLazy();

        Container.BindInterfacesAndSelfTo<TestPlayerControlAnimation>()
           .AsSingle()
           .WithArguments(_animatorHand, _settingsPlayer)
           .NonLazy();
    }

    private void BindInput()
    {
        Container.BindInterfacesAndSelfTo<PlayerInputControl>()
           .AsSingle()
           .NonLazy();

        Container.Bind<PlayerSystemActions>()
           .AsSingle()
           .NonLazy();
    }


    private void BindImporter()
    {
        Container.BindInterfacesAndSelfTo<ImporterAimMove>()
            .AsSingle()
            .WithArguments(_settingsPlayer)
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ImporterAimSensitivity>()
            .AsSingle()
            .WithArguments(_settingsPlayer)
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ImporterMoveAnimation>()
           .AsSingle()
           .NonLazy();
    }

    private void BindImporterUI()
    {
        Container.BindInterfacesAndSelfTo<ImporterInteractebleUI>()
           .AsSingle()
           .NonLazy();
    }

}

public enum TypePlay
{
    Test,
    Game
}
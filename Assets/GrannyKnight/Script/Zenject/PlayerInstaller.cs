using Refactor;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;


public class PlayerInstaller : MonoInstaller
{
    [Header("Continue")]
    [SerializeField] private PlayerCharacter _playerCharecter;
    [SerializeField] private SettingsPlayer _settingsPlayer;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _groundPoint;

    [Header("PlayerLook/PlayerInteracteble")]
    [SerializeField] private Transform _headSlot;

    [Header("PlayerAim")]
    [SerializeField] private CinemachineCamera cinemachineCamera;

    [Header("PlayerControlAnimation")]
    [SerializeField] private Animator _armorHandAnimator;
    [SerializeField] private Animator _glovesHandAnimator;


    public override void InstallBindings()
    {
        BindPlayer();
        BindInput();
        BindImporter();
        BindUI();
    }

    private void BindPlayer()
    {
        Container.Bind<PlayerCharacter>()
        .FromInstance(_playerCharecter)
        .AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerMove>()
            .AsSingle()
            .WithArguments(_settingsPlayer, _characterController, _groundPoint)
            .NonLazy();

        Container.Bind<PlayerLook>()
            .AsSingle()
            .WithArguments(_settingsPlayer, _headSlot, _playerCharecter.transform)
            .NonLazy();

        Container.BindInterfacesAndSelfTo<PlayerAim>()
            .AsSingle()
            .WithArguments(_settingsPlayer, cinemachineCamera)
            .NonLazy();

        Container.BindInterfacesAndSelfTo<TestPlayerInteracteble>()
            .AsSingle()
            .WithArguments(_settingsPlayer, _headSlot)
            .NonLazy();

        Container.Bind<TestPlayerControlAnimation>()
           .AsSingle()
           .WithArguments(_armorHandAnimator, _glovesHandAnimator, _settingsPlayer)
           .NonLazy();

        Container.Bind<PlayerWeapon>()
           .AsSingle()
           .NonLazy();

        Container.Bind<PlayerHealth>()
           .AsSingle()
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

        Container.BindInterfacesAndSelfTo<ImporterPlayerWeaponAnimation>()
           .AsSingle()
           .NonLazy();
    }

    private void BindUI()
    {
        Container.BindInterfacesAndSelfTo<ImporterInteractebleUI>()
           .AsSingle()
           .NonLazy();

        Container.BindInterfacesAndSelfTo<TestPlayerSetUpUi>()
          .AsSingle()
          .NonLazy();
    }
}
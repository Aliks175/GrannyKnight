using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SleepInstaller : MonoInstaller
{
    [Header("UIProtectionGarden")]
    [SerializeField] private Image _blackout;
    [SerializeField] private int[] _stageHealth;
    [SerializeField] private float _timeImmunity;
    [SerializeField] private float _timeToRegen;
    [SerializeField] private int _healthPlayer;

    [Header("ProtectionGardenSettings")]
    [SerializeField] private TargetMonsterGarden _prefMonster;
    [SerializeField] private TargetBossGarden _prefBoss;

    public override void InstallBindings()
    {
        BindQuestBubblesDestruction();
        BindQuestProtectionGarden();
        BindPlayerStrategyHealth();
        BindFactory();
    }

    private void BindPlayerStrategyHealth()
    {
        Container.BindInterfacesAndSelfTo<BlackoutScreen>()
          .AsSingle()
          .WithArguments(_blackout, _healthPlayer, _timeImmunity, _timeToRegen, _stageHealth)
          .NonLazy();
    }

    private void BindFactory()
    {
        Container.BindInterfacesAndSelfTo<FactoryMonsterGarden>()
         .AsSingle()
         .WithArguments(_prefMonster, _prefBoss)
         .NonLazy();
    }

    private void BindQuestBubblesDestruction()
    {
        Container.Bind<ControlBubbles>()
           .AsSingle();
    }

    private void BindQuestProtectionGarden()
    {
        Container.BindInterfacesAndSelfTo<ControlProtectionGarden>()
           .AsSingle();

        Container.Bind<DestroyEnemyGarden>()
          .AsSingle();

        Container.Bind<DestroyBossGarden>()
        .AsSingle();

        Container.BindInterfacesAndSelfTo<MoveToPlayer>()
        .AsSingle();
    }
}
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SleepInstaller : MonoInstaller
{
    [Header("UIProtectionGarden")]
    [SerializeField] private Image _imageScroller;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _timeImmunity;
    [SerializeField] private float _startHealth;
    //[SerializeField] private int[] _stageHealth;
    //[SerializeField] private int _healthPlayer;

    [Header("ProtectionGardenSettings")]
    [SerializeField] private TargetMonsterGarden _prefMonster;
    [SerializeField] private TargetBossGarden _prefBoss;

    [Header("FactoryBulletSettings")]
    [SerializeField] private Bullet _prefBullet;
    [SerializeField] private float _timeDisable;
    [SerializeField] private int _sizePool;
    [SerializeField] private int _damage;
    private const string _nameParent = "DarkBulletPool";

    public override void InstallBindings()
    {
        BindQuestBubblesDestruction();
        BindQuestProtectionGarden();
        BindPlayerStrategyHealth();
        BindFactory();
    }

    private void BindPlayerStrategyHealth()
    {
        //(Image image, float startHealth, float timeImmunity)
        Container.BindInterfacesAndSelfTo<HealthBarScreen>()
          .AsSingle()
          .WithArguments(_imageScroller, _canvas, _startHealth, _timeImmunity)
          .NonLazy();


        //Container.BindInterfacesAndSelfTo<BlackoutScreen>()
        //  .AsSingle()
        //  .WithArguments(_blackout, _healthPlayer, _timeImmunity, _timeToRegen, _stageHealth)
        //  .NonLazy();
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
        Container.Bind<ControlTarget>()
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

        Container.BindInterfacesAndSelfTo<FairyAnimation>()
        .AsSingle();

        Container.BindInterfacesAndSelfTo<MoveToPlayer>()
            .AsTransient();

        Container.Bind<FairySimpleMove>()
        .AsTransient();

        Container.Bind<FairyAttack>()
       .AsTransient();

        Container.Bind<FactoryBullet>()
            .AsSingle()
            .WithArguments(_prefBullet, _sizePool, _damage, _timeDisable, _nameParent)
            .WhenInjectedInto<FairyAttack>();


        Container.Bind<MoveToPlayer>()
            .AsTransient()
            .WhenInjectedInto<TargetDustGarden>();

        Container.Bind<DustAttack>()
           .AsTransient();

    }
}
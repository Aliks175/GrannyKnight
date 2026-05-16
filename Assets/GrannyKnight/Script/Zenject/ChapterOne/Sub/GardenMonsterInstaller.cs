using UnityEngine;
using Zenject;

public class GardenMonsterInstaller : MonoInstaller
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _body;
    [SerializeField] private TargetMonsterGarden _monsterGarden;
    [SerializeField] private DangerZone _dangerZone;

    public override void InstallBindings()
    {
        BindMonsterSub();
        BindImporter();
    }

    private void BindMonsterSub()
    {
        Container.Bind<HealthMonsterGarden>()
           .AsSingle();

        Container.Bind<AttackMonsterGarden>()
           .AsSingle()
           .WithArguments(_body);

        Container.Bind<AnimationMonsterGarden>()
            .AsSingle()
            .WithArguments(_spriteRenderer, _body);

        Container.Bind<DangerZone>()
           .FromInstance(_dangerZone)
           .AsSingle();
    }

    private void BindImporter()
    {
        Container.BindInterfacesAndSelfTo<ImporterAnimationMonster>()
        .AsSingle();

        Container.BindInterfacesAndSelfTo<ImporterMonsterDestroy>()
        .AsSingle()
        .WithArguments(_monsterGarden);
    }
}
using UnityEngine;
using Zenject;

public class GardenMonsterInstaller : MonoInstaller
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _body;
    [SerializeField] private TargetMonsterGarden _monsterGarden;

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
           .AsSingle();

        Container.Bind<AnimationMonsterGarden>()
            .AsSingle()
            .WithArguments(_spriteRenderer, _body);
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
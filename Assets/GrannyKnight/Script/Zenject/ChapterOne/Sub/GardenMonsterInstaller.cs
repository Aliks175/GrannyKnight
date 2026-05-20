using UnityEngine;
using Zenject;

public class GardenMonsterInstaller : MonoInstaller
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _body;
    [SerializeField] private TargetMonsterGarden _monsterGarden;
    [SerializeField] private DangerZone _dangerZone;
    [SerializeField] private float _damage;

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
           .WithArguments(_body, _damage);

        Container.Bind<AnimationMonsterGarden>()
            .AsSingle()
            .WithArguments(_spriteRenderer, _body, _particleSystem);

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
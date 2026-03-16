using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Refactor
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private List<TestBazeWeapon> BazeWeapon;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _handSlot;

        public override void InstallBindings()
        {
            BindFactory();
            BindSystem();
            BindImporter();
        }

        private void BindImporter()
        {
            Container.BindInterfacesAndSelfTo<ImporterPlayerWeaponSystem>()
           .AsSingle()
           .NonLazy();
        }

        private void BindFactory()
        {
            Container.BindInterfacesAndSelfTo<FactoryWeapon>()
           .AsSingle()
           .WithArguments(BazeWeapon, _handSlot)
           .NonLazy();
        }

        private void BindSystem()
        {
            Container.BindInterfacesAndSelfTo<TestWeaponSystem>()
           .AsSingle()
           .NonLazy();

            Container.BindInterfacesAndSelfTo<ShootingRaycast>()
           .AsSingle()
           .WithArguments(_head)
           .NonLazy();

            Container.BindInterfacesAndSelfTo<ShootingPhysics>()
          .AsSingle()
          .WithArguments(_head)
          .NonLazy();
        }
    }
}
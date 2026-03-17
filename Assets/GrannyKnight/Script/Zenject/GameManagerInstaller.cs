using UnityEngine;
using Zenject;

    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCharacter _prefPlayer;

        public override void InstallBindings()
        {
            BindFactoryPlayer();
        }

        private void BindFactoryPlayer()
        {
            Container.Bind<FactoryPlayer>()
            .AsSingle()
            .WithArguments(_prefPlayer)
            .NonLazy();
        }
    }

using UnityEngine;
using Zenject;

namespace Refactor
{
    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField] private TestPlayerCharacter _prefPlayer;

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
}
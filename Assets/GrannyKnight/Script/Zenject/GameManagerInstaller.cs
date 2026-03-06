using UnityEngine;
using Zenject;

namespace Refactor
{
    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField] private TestPlayerCharacter testPlayerCharacter;
        public override void InstallBindings()
        {
        }
    }
}
using UnityEngine;
using Zenject;

namespace Refactor
{
    public class FactoryPlayer
    {
        private DiContainer _container;
        private TestPlayerCharacter _testPlayerCharacter;

        public FactoryPlayer(DiContainer container, TestPlayerCharacter testPlayerCharacter)
        {
            _container = container;
            _testPlayerCharacter = testPlayerCharacter;
        }

        public void Create(Transform transform)
        {
            _container.InstantiatePrefabForComponent<TestPlayerCharacter>(_testPlayerCharacter, transform.position, Quaternion.identity, null);
        }

        //public PlayerCharecter CreatePlayer(TypeCharecter typeCharecter, Transform transform)
        //{
        //    PlayerCharecter playerCharecter = null;
        //    if (typeCharecter == TypeCharecter.pistolCharecter)
        //    {
        //        playerCharecter = _container.InstantiatePrefabForComponent<PlayerCharecter>(_playerPistol, transform.position, Quaternion.identity, null);
        //    }
        //    else if (typeCharecter == TypeCharecter.shotgunCharecter)
        //    {
        //        playerCharecter = _container.InstantiatePrefabForComponent<PlayerCharecter>(_playerShotgun, transform.position, Quaternion.identity, null);
        //    }
        //    return playerCharecter;
        //}
    }
}
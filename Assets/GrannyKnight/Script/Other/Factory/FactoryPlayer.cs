using UnityEngine;
using Zenject;

    public class FactoryPlayer
    {
        private DiContainer _container;
        private PlayerCharacter _testPlayerCharacter;

        public FactoryPlayer(DiContainer container, PlayerCharacter testPlayerCharacter)
        {
            _container = container;
            _testPlayerCharacter = testPlayerCharacter;
        }

        public PlayerCharacter Create(Transform transform)
        {
           return _container.InstantiatePrefabForComponent<PlayerCharacter>(_testPlayerCharacter, transform.position, Quaternion.identity, null);
        }
    }
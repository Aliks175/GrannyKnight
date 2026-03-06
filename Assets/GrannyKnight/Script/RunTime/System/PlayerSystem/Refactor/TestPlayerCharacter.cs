using UnityEngine;
using Zenject;

namespace Refactor
{
    public class TestPlayerCharacter : MonoBehaviour
    {
        public TestPlayerMove PlayerMove => _playerMove;
        public TestPlayerLook PlayerLook => _playerLook;
        public TestPlayerAim PlayerAim => _playerAim;
        private TestPlayerMove _playerMove;
        private TestPlayerLook _playerLook;
        private TestPlayerAim _playerAim;

        [Inject]
        public void Construct(TestPlayerMove playerMove, TestPlayerLook playerLook, TestPlayerAim playerAim)
        {
            _playerMove = playerMove;
            _playerLook = playerLook;
            _playerAim = playerAim;
        }
    }
}
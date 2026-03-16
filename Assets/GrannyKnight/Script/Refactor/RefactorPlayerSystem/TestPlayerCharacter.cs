using UnityEngine;
using Zenject;

namespace Refactor
{
    public class TestPlayerCharacter : MonoBehaviour
    {
        public TestPlayerMove PlayerMove => _playerMove;
        public TestPlayerLook PlayerLook => _playerLook;
        public TestPlayerAim PlayerAim => _playerAim;
        public TestPlayerWeapon PlayerWeapon => _playerWeapon;
        
        private TestPlayerMove _playerMove;
        private TestPlayerWeapon _playerWeapon;
        private TestPlayerLook _playerLook;
        private TestPlayerAim _playerAim;

        [Inject]
        public void Construct(TestPlayerMove playerMove, TestPlayerLook playerLook, TestPlayerAim playerAim, TestPlayerWeapon playerWeapon)
        {
            _playerMove = playerMove;
            _playerLook = playerLook;
            _playerAim = playerAim;
            _playerWeapon = playerWeapon;
        }

        private void Start()
        {
            GiveWeapon(0);
        }

        public void GiveWeapon(int slotNumber)
        {
            PlayerWeapon.GiveWeapon((EquipHand)slotNumber);
        }
    }
}
using System;
using UnityEngine;

namespace Refactor
{
    public class TestPlayerWeapon
    {
        private FactoryWeapon _factoryWeapon;
        private TestWeapon _equipWeapon;
        private OnDataEquipHand _dataEquipHand;
        public event EventHandler<OnDataEquipHand> OnChangeHand;

        public TestPlayerWeapon(FactoryWeapon factoryWeapon)
        {
            _factoryWeapon = factoryWeapon;
            _dataEquipHand = new() { EquipHand = EquipHand.none };
        }

        public void GiveWeapon(EquipHand slotNumber)
        {
            if (!CheckHand(slotNumber)) { return; }
            switch (slotNumber)
            {
                case EquipHand.ArmorHand:
                case EquipHand.GlovesHand:
                    _equipWeapon = null;
                    break;
                default:
                    TestWeapon tempWeapon = _factoryWeapon.GetWeapon(slotNumber);
                    EquipWeapon(tempWeapon);
                    break;
            }
            SendChangeHand(slotNumber);
        }

        private void EquipWeapon(TestWeapon tempWeapon)
        {
            if (tempWeapon != null)
            {
                _equipWeapon = tempWeapon;
            }
        }

        private void SendChangeHand(EquipHand equipHand)
        {
            _dataEquipHand.EquipHand = equipHand;
            _dataEquipHand.Weapon = _equipWeapon;
            OnChangeHand?.Invoke(this, _dataEquipHand);
        }

        private bool CheckHand(EquipHand equipHand)
        {
            bool isNewHand = true;
            if (equipHand == _dataEquipHand.EquipHand)
            {
                Debug.Log("CheckHand == EquipHand");
                isNewHand = false;
            }
            return isNewHand;
        }
    }

    public class OnDataEquipHand : EventArgs
    {
        public EquipHand EquipHand;
        public TestWeapon Weapon;
    }
}
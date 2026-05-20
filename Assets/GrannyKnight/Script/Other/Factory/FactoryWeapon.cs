using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryWeapon : IInitializable
{
    private List<TestBazeWeapon> _bazeWeapons;

    private List<TestWeapon> _weaponList;

    private Transform _handSlot;

    public FactoryWeapon(List<TestBazeWeapon> bazeWeapons, Transform handSlot)
    {
        _bazeWeapons = bazeWeapons;
        _weaponList = new();
        _handSlot = handSlot;
    }

    public void Initialize()
    {
        CreateWeapon();
    }

    public TestWeapon GetWeapon(EquipHand equipHand)
    {
        int weaponID = ConvertTypeWeaponForId(equipHand);
        return FindWeapon(weaponID);
    }

    private int ConvertTypeWeaponForId(EquipHand equipHand)
    {
        int id = 0;
        switch (equipHand)
        {
            case EquipHand.ArmorHand:
            case EquipHand.GlovesHand:
                id = (int)TypeWeapon.none;
                break;
            case EquipHand.SlingshotHand:
                id = (int)TypeWeapon.Sling;
                break;
            case EquipHand.PodmetatusHand:
                id = (int)TypeWeapon.Metlomet;
                break;
            case EquipHand.EasterEggsHand:
                id = (int)TypeWeapon.EasterEgg;
                break;
            case EquipHand.SwordHand:
                id = (int)TypeWeapon.Sword;
                break;
        }
        return id;
    }

    private TestWeapon FindWeapon(int weaponID)
    {
        TestWeapon testWeapon = null;
        foreach (var weapon in _weaponList)
        {
            if (weapon.ID == weaponID)
            {
                testWeapon = weapon;
                return testWeapon;
            }
        }
        return testWeapon;
    }

    private void CreateWeapon()
    {
        for (int i = 0; i < _bazeWeapons.Count; i++)
        {
            //DataWeapon dataWeapon = _bazeWeapons[i].DataWeapon;
            DataWeapon dataWeapon = CreateDataWeapon(_bazeWeapons[i].DataWeapon);
            if (dataWeapon == null)
            {
                Debug.LogError($"Not Found WeaponSetting");
                break;
            }


            DataItem dataItem = CreateModelItem(_bazeWeapons[i].DataItem);
            TestWeapon testWeapon = new TestWeapon(dataWeapon, dataItem);
            _weaponList.Add(testWeapon);
        }
    }

    private DataWeapon CreateDataWeapon(WeaponSetting weaponSetting)
    {
        DataWeapon dataWeapon = null;
        if (weaponSetting.TypeShootingSystem == TypeShootingSystem.Raycast)
        {
            DataWeaponRaycast tempWeaponRaycast = new DataWeaponRaycast(weaponSetting);
            dataWeapon = tempWeaponRaycast;
        }
        else if (weaponSetting.TypeShootingSystem == TypeShootingSystem.Physics)
        {
            DataWeaponPhysics tempWeaponPhysics = new DataWeaponPhysics(weaponSetting);
            dataWeapon = tempWeaponPhysics;
        }
        else if (weaponSetting.TypeShootingSystem == TypeShootingSystem.Melee)
        {
            DataWeaponMelee tempWeaponPhysics = new DataWeaponMelee(weaponSetting);
            dataWeapon = tempWeaponPhysics;
        }


        return dataWeapon;
    }

    private DataItem CreateModelItem(DataItem dataItem)
    {
        ShootPoint tempModel = GameObject.Instantiate(dataItem.Model, _handSlot);
        tempModel.gameObject.SetActive(false);
        DataItem tempDataItem = new DataItem()
        {
            Id = dataItem.Id,
            Model = tempModel,
            Name = dataItem.Name,
        };
        return tempDataItem;
    }
}
using UnityEngine;

public class TestWeapon
{
    public TypeWeapon TypeWeapon => _dataWeapon.TypeWeapon;
    public TypeShootingSystem TypeShootingSystem => _dataWeapon.TypeShootingSystem;
    public DataWeapon DataWeapon => _dataWeapon;
    public Transform FirePoint => _dataItem.Model.FirePoint;
    public int ID => _dataItem.Id;

    private DataWeapon _dataWeapon;
    private DataItem _dataItem;

    public TestWeapon(DataWeapon dataWeapon, DataItem dataItem)
    {
        _dataWeapon = dataWeapon;
        _dataItem = dataItem;
    }
}
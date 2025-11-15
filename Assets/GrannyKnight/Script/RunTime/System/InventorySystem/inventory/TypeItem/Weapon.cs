using System;

public class Weapon : Item
{
    private DataWeapon DataWeapon;
    public TypeWeapon TypeWeapon => DataWeapon.TypeWeapon;
    public int Damage => DataWeapon.Damage;
    public float TimeWaitFire => DataWeapon.TimeWaitFire;
    public float Range => DataWeapon.Range;
    public bool IsAutoFire => DataWeapon.IsAutoFire;

    public override void Initialization(BazeItem bazeItem, IPlayerDatable characterData)
    {
        base.Initialization(bazeItem, characterData);
        if (bazeItem is BazeWeapon weapon)
        {
            DataWeapon = weapon.DataWeapon;
            characterData.ChooseWeapon.SetWeapon(DataWeapon.TypeWeapon, this);
        }
    }
}

public enum TypeWeapon
{
    none,
    Pistol,
    Stick,
    Grenade
}

[Serializable]
public struct DataWeapon
{
    public TypeWeapon TypeWeapon;
    public int Damage;
    public float TimeWaitFire;
    public float Range;
    public bool IsAutoFire;
}
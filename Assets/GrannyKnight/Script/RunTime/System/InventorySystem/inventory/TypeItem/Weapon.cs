using System;
using UnityEngine;

public class Weapon : Item
{
    private DataWeapon DataWeapon;
    public TypeWeapon TypeWeapon => DataWeapon.TypeWeapon;
    public  TypeShootPhysics TypeShoot => DataWeapon.TypeShoot;
    public int Damage => DataWeapon.Damage;
    public float TimeWaitFire => DataWeapon.TimeWaitFire;
    public float Range => DataWeapon.Range;
    public bool IsAutoFire => DataWeapon.IsAutoFire;
    public float MaxForce => DataWeapon.MaxForce;
    public float ForceMultiplier => DataWeapon.ForceMultiplier;
    public float KoefCharge => DataWeapon.KoefCharge;
    public GameObject ArrowPrefab => DataWeapon.ArrowPrefab;

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
    EasterEgg,
    Metlomet,
    Sling,
}

public enum TypeShootPhysics
{
    Raycast,
    Physics
}

[Serializable]
public struct DataWeapon
{
    public TypeWeapon TypeWeapon;
    public TypeShootPhysics TypeShoot;
    public int Damage;
    public float TimeWaitFire;
    [Header("For Raycast weapon")]
    public float Range;
    public bool IsAutoFire;
    [Header("For Physics weapon")]
    public float MaxForce;
    public float ForceMultiplier;
    public float KoefCharge;
    public GameObject ArrowPrefab;
}
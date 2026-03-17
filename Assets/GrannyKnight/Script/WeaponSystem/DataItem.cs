using System;
using UnityEngine;

[Serializable]
public struct DataItem
{
    public ShootPoint Model;
    public string Name;
    public int Id;
}

[Serializable]
public struct DataRaycastWeapon
{
    public TypeWeapon TypeWeapon;
    public int Damage;
    public float TimeWaitFire;
    [Header("For Raycast weapon")]
    public float Range;
    public bool IsAutoFire;
}

[Serializable]
public struct DataPhysicsWeapon
{
    public TypeWeapon TypeWeapon;
    public int Damage;
    public float TimeWaitFire;
    [Header("For Physics weapon")]
    public float MaxForce;
    public float ForceMultiplier;
    public float KoefCharge;
    public GameObject ArrowPrefab;
}
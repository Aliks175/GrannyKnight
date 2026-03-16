
using System;
using UnityEngine;

namespace Refactor
{
    [Serializable]
    public struct DataItem
    {
        public ShootPoint Model; // Точка спавна снарядов либо эфектов оружия 
        public string Name;
        public int Id;
        //public bool IsStackable;
        //public int CountItem;
        //public TypeUse TypeUse;
        //public TypeItem TypeItem;
        //[TextArea(2, 5)] public string DescriptionItem;
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
}
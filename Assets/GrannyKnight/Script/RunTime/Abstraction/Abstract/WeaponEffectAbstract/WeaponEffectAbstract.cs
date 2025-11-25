using UnityEngine;

public abstract class WeaponEffectAbstract : MonoBehaviour
{
    public abstract void Initialization(IFireble testWeapon);
    public abstract Animator AnimatorWeapon { get; }
    public abstract int IdWeapon { get; }
}

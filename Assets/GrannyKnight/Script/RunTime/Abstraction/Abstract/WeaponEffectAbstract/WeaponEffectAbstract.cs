using UnityEngine;

public abstract class WeaponEffectAbstract : MonoBehaviour
{
    public abstract void Initialization(IFireble testWeapon, ControlViewMark controlViewMark);
    public abstract Animator AnimatorWeapon { get; }
    public abstract int IdWeapon { get; }
}

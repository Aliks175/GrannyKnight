using UnityEngine;

public abstract class WeaponEffectAbstract : MonoBehaviour
{
    public abstract void DisableSound();
    //public abstract void Initialization(IFireble testWeapon);
    public abstract Animator AnimatorWeapon { get; }
    public abstract int IdWeapon { get; }
    public virtual void OnAttackOne() { }
    public virtual void OnBlock(bool isActive) { }
    public virtual void OnAttackTwo() { }
    public virtual void OnEndShoot() { }
    public virtual void OnCharge() { }
}

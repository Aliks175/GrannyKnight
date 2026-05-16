using UnityEngine;

public interface IShootPointeble
{
    public Transform FirePoint { get; }
    public Transform FirePointTwo { get; }
    public Animator AnimatorHand { get; }
    public WeaponEffectAbstract WeaponEffectAbstract { get; }
}

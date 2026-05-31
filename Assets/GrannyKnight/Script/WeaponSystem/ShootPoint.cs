using UnityEngine;

public class ShootPoint : MonoBehaviour, IShootPointeble
{
    #region PublicField
    public Transform FirePoint => _firePoint;

    public Transform FirePointTwo => _firePointTwo == null ? _firePoint : _firePointTwo;

    public Animator AnimatorHand => _animatorHand;

    public WeaponEffectAbstract WeaponEffectAbstract => _weaponEffectAbstract;
    #endregion

    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _firePointTwo;
    [SerializeField] Animator _animatorHand;
    [SerializeField] WeaponEffectAbstract _weaponEffectAbstract;

   
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_firePoint.position, 1);
    }
}
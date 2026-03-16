using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    public Transform FirePoint => _firePoint;
    [SerializeField] private Transform _firePoint;
}
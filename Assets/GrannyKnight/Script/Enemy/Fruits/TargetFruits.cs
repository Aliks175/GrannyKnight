using UnityEngine;

public class TargetFruits : MonoBehaviour, IHealtheble
{
    [SerializeField] private Rigidbody rb;

    public void TakeDamage(float damage)
    {
        rb.isKinematic = false;
    }

    
}

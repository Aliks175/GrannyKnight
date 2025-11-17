using UnityEngine;

public class TestTarget : MonoBehaviour, IHealtheble
{
    [SerializeField] private float _startHealth = 50f;
    [SerializeField] private GameObject _dropItem;
    [SerializeField] private Transform _dropPoint;
    private float _health;

    private void Start()
    {
        _health = _startHealth;
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Abs(damage);
        _health -= damage;
        if (_health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        LootDrop();
        Destroy(gameObject);
    }

    private void LootDrop()
    {
        GameObject tempDrop = Instantiate(_dropItem, _dropPoint.position, Quaternion.identity);
    }
}

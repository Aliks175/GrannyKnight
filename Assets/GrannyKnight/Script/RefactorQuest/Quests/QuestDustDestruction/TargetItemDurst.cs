using UnityEngine;
using Zenject;

public class TargetItemDurst : MonoBehaviour, IHealtheble, ITarget
{
    public GameObject Body => gameObject;
    [SerializeField] private float _maxHealth;
    private ControlTarget _controlTarget;
    private float _health;
    private bool _isAlife => _health>0;

    [Inject]
    public void Construct(ControlTarget controlBubbles)
    {
        _controlTarget = controlBubbles;
        _controlTarget.AddBubbles(this);
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CheckHealth(damage);
    }

    private void CheckHealth(float damage)
    {
        if (!_isAlife) { return; }
        damage = Mathf.Abs(damage);
        _health -= damage;
        if (_health > 0)
        {
            //OnHit?.Invoke();
            // Анимация
        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        _controlTarget.AddCountTargetDestruction();
        gameObject.SetActive(false);
    }
}
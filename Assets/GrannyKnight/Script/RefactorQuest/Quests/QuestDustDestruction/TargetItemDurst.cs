using System.Collections;
using UnityEngine;
using Zenject;

public class TargetItemDurst : MonoBehaviour, IHealtheble, ITarget
{
    public GameObject Body => gameObject;
    [SerializeField] private ParticleSystem _lifeEffect;
    [SerializeField] private ParticleSystem _deadEffect;
    [SerializeField] private float _maxHealth;
    private ControlTarget _controlTarget;
    private float _health;
    private bool _isAlife => _health>0;

    [Inject]
    public void Construct(ControlTarget controlBubbles)
    {
        _controlTarget = controlBubbles;
        _controlTarget.AddTarget(this);
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
            _deadEffect.Play();
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
        _lifeEffect.Stop();
        Debug.Log("main.duration" + _deadEffect.main.duration);
        _deadEffect.Play();
        StartCoroutine(WaitEffect(_deadEffect.main.duration));
        _controlTarget.AddCountTargetDestruction();
    }

    private IEnumerator WaitEffect(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }


}
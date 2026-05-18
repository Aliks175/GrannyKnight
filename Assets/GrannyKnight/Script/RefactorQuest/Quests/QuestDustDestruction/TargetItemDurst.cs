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
    private Coroutine _coroutine;
    private float _health;
    private bool _isAlife => _health > 0;

    [Inject]
    public void Construct(ControlTarget controlBubbles)
    {
        _controlTarget = controlBubbles;
        _controlTarget.OnStartQuest += Start;
    }

    private void OnDisable()
    {
        _controlTarget.OnStartQuest -= Start;
    }

    private void Start()
    {
        _controlTarget.OnStopQuest += OnStopQuest;
        gameObject.SetActive(true);
        _health = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("TakeDamage");
        CheckHealth(damage);
    }

    private void OnStopQuest()
    {
        _health = -1f;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _controlTarget.OnStopQuest -= OnStopQuest;
        Destroy(gameObject, 1f);
    }


    private void CheckHealth(float damage)
    {
        if (!_isAlife) { return; }
        damage = Mathf.Abs(damage);
        _health -= damage;
        if (_health > 0)
        {
            _deadEffect.Play();
        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        _controlTarget.OnStopQuest -= OnStopQuest;
        _lifeEffect.Stop();
        Debug.Log("main.duration" + _deadEffect.main.duration);
        _deadEffect.Play();
        _coroutine = StartCoroutine(WaitEffect(_deadEffect.main.duration));
        _controlTarget.AddCountTargetDestruction();
    }

    private IEnumerator WaitEffect(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        OnStopQuest();
    }


}
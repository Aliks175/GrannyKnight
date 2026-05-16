using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody Rigidbody => _rigidbody;
    [SerializeField] private Rigidbody _rigidbody;
    private WaitForSeconds _waitDisable;
    private WaitForSeconds _waitTakeDamage;
    private Coroutine _coroutine;
    private int _damage;
    private bool _dead;

    private void OnTriggerEnter(Collider other)
    {
        if (_dead) { return; }
        if (other.gameObject.TryGetComponent(out IHealtheble target))
        {
            Debug.Log("_waitTakeDamage");
            target.TakeDamage(_damage);
            Disable();
        }
    }

    public void Shoot()
    {
        CheckShoot();
    }

    public void Initialization(float timeDisable, int damage)
    {
        _waitDisable = new WaitForSeconds(timeDisable);
        _waitTakeDamage = new WaitForSeconds(0.5f);
        _damage = damage;
    }


    private IEnumerator WaitDisable(WaitForSeconds waitForSeconds)
    {
        yield return waitForSeconds;
        gameObject.SetActive(false);
    }

    private void CheckShoot()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _dead = false;
        _coroutine = StartCoroutine(WaitDisable(_waitDisable));
    }

    private void Disable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _dead = true;
        _coroutine = StartCoroutine(WaitDisable(_waitTakeDamage));
    }
}
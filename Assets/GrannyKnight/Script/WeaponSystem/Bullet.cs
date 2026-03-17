using System.Collections;
using UnityEngine;

    public class Bullet : MonoBehaviour
    {
        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] private Rigidbody _rigidbody;
        private WaitForSeconds _waitDisable;
        private Coroutine _coroutine;
        private int _damage;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IHealtheble target))
            {
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
            _damage = damage;
        }


        private IEnumerator WaitDisable()
        {
            yield return _waitDisable;
            gameObject.SetActive(false);
        }

        private void CheckShoot()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(WaitDisable());
        }

        private void Disable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            gameObject.SetActive(false);
        }
    }
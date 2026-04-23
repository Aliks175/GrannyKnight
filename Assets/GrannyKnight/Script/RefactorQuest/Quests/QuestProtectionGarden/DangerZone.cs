using System.Collections;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    private WaitForSeconds _waitAttack;
    private const float _waitTime = 2f;
    private bool _isPlay;

    private void Start()
    {
        _waitAttack = new WaitForSeconds(_waitTime);
        _isPlay = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Damage(other);
    }

    private void Damage(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerCharacter player))
        {
            Debug.Log("Damage");
            if (!_isPlay) return;
            player.TakeDamage(1);
            _isPlay = false;
            StartCoroutine(WaitAttack());
        }
    }

    private IEnumerator WaitAttack()
    {
        yield return _waitAttack;
        _isPlay = true;
    }
}
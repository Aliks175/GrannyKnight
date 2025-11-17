using UnityEngine;
using UnityEngine.Events;

public class ActiveDoEnter : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    public UnityEvent UnityEvent;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start");
        Debug.Log(other.gameObject.name);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5, _playerLayer);
        if (colliders.Length > 0 && colliders != null)
        {
            UnityEvent?.Invoke();
        }
    }
}

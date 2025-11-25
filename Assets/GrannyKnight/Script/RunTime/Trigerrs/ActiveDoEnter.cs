using UnityEngine;
using UnityEngine.Events;

public class ActiveDoEnter : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    public UnityEvent UnityEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Start");
            Debug.Log(other.gameObject.name);
            UnityEvent?.Invoke();
        }
    }
}
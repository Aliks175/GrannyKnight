using UnityEngine;
using UnityEngine.Events;

public class TargetDrag : MonoBehaviour
{
    [SerializeField] private UnityEvent _events;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.tag  == "Draggable")
        {
            Destroy(other.gameObject);
            Debug.Log("Coins Add");       
            _events?.Invoke();
        }
    }
}

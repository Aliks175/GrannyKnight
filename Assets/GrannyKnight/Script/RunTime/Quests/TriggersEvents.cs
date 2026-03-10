using UnityEngine;
using UnityEngine.Events;

public class TriggersEvents : MonoBehaviour
{
    [Header("Вошёл в триггер")]
    [SerializeField] private string _tagEnter = "Player";
    [SerializeField] private UnityEvent _eventsEnter;
    [Header("Стоит в триггере")]
    [SerializeField] private string _tagStay = "Player";
    [SerializeField] private UnityEvent _eventsStay;
    [Header("Вышел из триггера")]
    [SerializeField] private string _tagExit = "Player";
    [SerializeField] private UnityEvent _eventsExit;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagEnter))  _eventsEnter?.Invoke();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_tagStay)) _eventsStay?.Invoke();
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tagExit)) _eventsExit?.Invoke();
    }
}

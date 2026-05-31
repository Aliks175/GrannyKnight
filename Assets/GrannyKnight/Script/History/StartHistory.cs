using System.Collections;
using UnityEngine;
using Zenject;

public class StartHistory : MonoBehaviour
{
    [SerializeField] private EventHistory _history;
    [SerializeField] private float _timeWait = 1f;
  
    private void Start()
    {
        StartCoroutine(WaitStartSecond());
    }

    private IEnumerator WaitStartSecond()
    {
        yield return new WaitForSeconds(_timeWait);
        _history.ActiveHistory();
    }
}
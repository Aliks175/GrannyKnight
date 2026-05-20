using System.Collections;
using UnityEngine;

public class StartHistory : MonoBehaviour
{
    [SerializeField] private EventHistory _history;

    private void Start()
    {
        StartCoroutine(WaitStartSecond());
    }

    private IEnumerator WaitStartSecond()
    {
        yield return new WaitForSeconds(1f);
        _history.ActiveHistory();
    }
}
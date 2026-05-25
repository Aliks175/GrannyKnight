using System.Collections;
using UnityEngine;
using Zenject;

public class StartHistory : MonoBehaviour
{
    [SerializeField] private EventHistory _history;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    private void Start()
    {
        _systemBuss.ReadySpawnPlayer();
        StartCoroutine(WaitStartSecond());
    }

    private IEnumerator WaitStartSecond()
    {
        yield return new WaitForSeconds(1f);
        _history.ActiveHistory();
    }
}
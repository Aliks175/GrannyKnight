using UnityEngine;
using Zenject;

public class SpawnPlayer : MonoBehaviour
{
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    public void Start()
    {
        _systemBuss.ReadySpawnPlayer();
    }

}

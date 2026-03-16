using Refactor;
using UnityEngine;
using Zenject;

public class SystemSpawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    private FactoryPlayer _factoryPlayer;

    [Inject]
    public void Construct(FactoryPlayer factoryPlayer)
    {
        _factoryPlayer = factoryPlayer;
    }

    private void Start()
    {
        Spawn(_spawnPoint);
    }

    private void Spawn(Transform spawnPoint)
    {
        _factoryPlayer.Create(spawnPoint);
    }
}
using Refactor;
using UnityEngine;
using Zenject;

public class SystemSpawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private bool _isSpawnAuto;
    private FactoryPlayer _factoryPlayer;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(FactoryPlayer factoryPlayer, SystemBuss systemBuss)
    {
        _factoryPlayer = factoryPlayer;
        _systemBuss = systemBuss;
    }

    private void OnDisable()
    {
        _systemBuss.OnReadySpawnPlayer -= ReadySpawnPlayer;
    }

    private void Awake()
    {
        _systemBuss.OnReadySpawnPlayer += ReadySpawnPlayer;
    }

    private void Start()
    {
        if ( _isSpawnAuto)
        {
            Spawn(_spawnPoint);
        }
    }

    private void ReadySpawnPlayer()
    {
        Spawn(_spawnPoint);
    }

    private void Spawn(Transform spawnPoint)
    {
        PlayerCharacter playerCharacter = _factoryPlayer.Create(spawnPoint);
    }
}
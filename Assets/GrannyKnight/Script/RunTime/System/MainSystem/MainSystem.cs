using System;
using UnityEngine;

public class MainSystem : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private WeaponSystem _weaponSystem;
    [SerializeField] private PlayerSystem _playerSystem;
    [SerializeField] private SoundSystem _soundSystem;
    [Header("Other")]
    [SerializeField] private InputControl _inputControl;
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private Transform _head;
    public static event Action OnUpdate;

    private void Start()
    {
        Initialization();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void Initialization()
    {
        _playerSystem.Initialization(_player, _head);
        _inputControl.Initialization(_weaponSystem);
        _weaponSystem.Initialization(_player.PlayerData, _head);
        _soundSystem.Initialization();
    }
}
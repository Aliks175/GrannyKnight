using System;
using UnityEngine;

public class MainSystem : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private WeaponSystem _weaponSystem;
    [SerializeField] private PlayerSystem _playerSystem;
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private UiSystem _uiSystem;
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
        _inputControl.Initialization(_weaponSystem, _inventorySystem);
        _playerSystem.Initialization(_player, _head);
        _weaponSystem.Initialization(_player.PlayerData, _head);
        _inventorySystem.Initialization(_player.PlayerData.Inventory);
        _uiSystem.Initialization(_player.PlayerData);
        _soundSystem.Initialization();
    }
}
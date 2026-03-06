using Refactor;
using System;
using UnityEngine;
using Zenject;

public class ImporterInteractebleUI : IDisposable, IInitializable
{
    private PlayerUi _playerUi;
    private TestPlayerInteracteble _playerInteracteble;

    public ImporterInteractebleUI(PlayerUi playerUi, TestPlayerInteracteble playerInteracteble)
    {
        _playerUi = playerUi;
        _playerInteracteble = playerInteracteble;
    }

    public void Dispose()
    {
        _playerInteracteble.OnChangeCurrentInteracteble -= OnChangeCurrentInteracteble;
    }

    public void Initialize()
    {
        _playerInteracteble.OnChangeCurrentInteracteble += OnChangeCurrentInteracteble;
    }

    private void OnChangeCurrentInteracteble(string description)
    {
        _playerUi.UpdateText(description);
    }
}

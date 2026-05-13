using Refactor;
using System;
using Zenject;

public class ImporterInteractebleUI : IDisposable, IInitializable
{
    private PlayerUi _playerUi;
    private SystemBuss _systemBuss;
    private PlayerInteracteble _playerInteracteble;

    public ImporterInteractebleUI(SystemBuss systemBuss, PlayerInteracteble playerInteracteble)
    {
        _systemBuss = systemBuss;
        _playerInteracteble = playerInteracteble;
    }

    public void Dispose()
    {
        _systemBuss.OnConstructPlayerUi -= OnConstructPlayerUi;
        _playerInteracteble.OnChangeCurrentInteracteble -= OnChangeCurrentInteracteble;
    }

    public void Initialize()
    {
        _systemBuss.OnConstructPlayerUi += OnConstructPlayerUi;
    }

    private void OnConstructPlayerUi(PlayerUi playerUi)
    {
        _playerUi = playerUi;
        _playerInteracteble.OnChangeCurrentInteracteble += OnChangeCurrentInteracteble;
    }

    private void OnChangeCurrentInteracteble(string description)
    {
        _playerUi.UpdateText(description);
    }
}
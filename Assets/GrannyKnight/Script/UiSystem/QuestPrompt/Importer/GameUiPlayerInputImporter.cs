using Refactor;
using UnityEngine;
using Zenject;

public class GameUiPlayerInputImporter : IInitializable
{
    private SystemBuss _systemBuss;
    private PlayerInputControl _playerInput;

    public GameUiPlayerInputImporter(SystemBuss systemBuss, PlayerInputControl playerInput)
    {
        _systemBuss = systemBuss;
        _playerInput = playerInput;
    }

    public void Initialize()
    {
        //_systemBuss.OnConstructGameUi += OnConstructGameUi;
    }

    private void OnConstructGameUi(GameUi ui)
    {
        //_systemBuss.OnConstructGameUi -= OnConstructGameUi;
        //_playerInput
        //_promptManager.Construct(ui.QuestPrompt);
    }
}

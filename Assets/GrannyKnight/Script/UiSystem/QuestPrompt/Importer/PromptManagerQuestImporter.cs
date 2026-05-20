using System;
using UnityEngine;
using Zenject;

public class PromptManagerQuestImporter : IInitializable
{
    private SystemBuss _systemBuss;
    private PromptManager _promptManager;

    public PromptManagerQuestImporter(SystemBuss systemBuss, PromptManager promptManager)
    {
        _systemBuss = systemBuss;
        _promptManager = promptManager;
    }

    public void Initialize()
    {
        _systemBuss.OnConstructPlayerUi += OnConstructPlayerUi;
    }

    private void OnConstructPlayerUi(PlayerUi ui)
    {
        _systemBuss.OnConstructPlayerUi -= OnConstructPlayerUi;
        _promptManager.Construct(ui.QuestPrompt);
    }
}

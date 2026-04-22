using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestProtectionGarden : Quest
{
    [SerializeField] private List<Transform> _posMonsterSpawn;
    private ControlProtectionGarden _controlProtectionGarden;
    private BlackoutScreen _playerStrategy;
    private SystemBuss _systemBuss;

    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;

    [Inject]
    public void Construct(ControlProtectionGarden controlProtectionGarden, BlackoutScreen playerStrategy, SystemBuss systemBuss)
    {
        _controlProtectionGarden = controlProtectionGarden;
        _playerStrategy = playerStrategy;
        _systemBuss = systemBuss;
    }

    private void OnDisable()
    {
        _controlProtectionGarden.OnEnd -= OnEndGame;
        _playerStrategy.OnEnd -= BadOver;
    }

    private void Start()
    {
        _playerStrategy.OnEnd += BadOver;
        _controlProtectionGarden.OnEnd += OnEndGame;
        WaitPlayer().Forget();
    }

    public override void StartQuest()
    {
        OnStart?.Invoke();
        _controlProtectionGarden.StartQuest(_posMonsterSpawn);
    }

    public override void StopQuest(QuestEnding quest)
    {
        OnEnd?.Invoke(quest);
        EndGame();
        Debug.Log("StopQuest QuestProtectionGarden ----");
    }

    private void BadOver()
    {
        StopQuest(QuestEnding.Bad);
    }

    private void OnEndGame()
    {
        StopQuest(QuestEnding.None);
    }

    private void EndGame()
    {
        _controlProtectionGarden.Dispose();
    }

    private async UniTaskVoid WaitPlayer()
    {
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        playerCharacter.SetStrategyHealtheble(_playerStrategy);
    }

}
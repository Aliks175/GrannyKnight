using System.Collections.Generic;
using UnityEngine;

public class FairyCreater : Quest
{
    [Header("Settings")]
    [SerializeField] private FairySettings _settings;
    [SerializeField] private TargetFairy _prefEnemy;
    [SerializeField] private int _valueWaves;
    [Header("TargetMove")]
    [SerializeField] private List<Transform> _targetsPoints;
    [SerializeField] private Transform _finalTarget;
    [Header("SpawnPointers")]
    [SerializeField] private List<Transform> _spawnPointers;
    private FairyTargets _fairyTargets;
    private int _wavesCount;
    private int _enemyForWave;
    private int _valueEnemyFollow;
    private bool _isActiveQuest = false;

    private void Start()
    {
        Instantiate();
    }

    public void Instantiate()
    {
        _fairyTargets = new FairyTargets()
        {
            FinalTarget = _finalTarget,
            TargetsPoints = _targetsPoints,
        };
    }

    public override void StartQuest()
    {
        if (_isActiveQuest) return;
        StartWaves();
        _isActiveQuest = true;
    }

    public override void StopQuest()
    {
        throw new System.NotImplementedException();
    }

    private void StartWaves()
    {
        if (_wavesCount >= _valueWaves) return;
        _wavesCount++;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        _enemyForWave = _settings.GetEnemyForWave(_wavesCount);

        FairyType fairyType;
        for (int i = 0; i < _enemyForWave; i++)
        {
            fairyType = _settings.GetEnemyType();
            CreateEnemy(fairyType);
        }
    }

    private void CreateEnemy(FairyType fairyType)
    {
        int indexSpawnPointer = Random.Range(0, _spawnPointers.Count);
        GameObject gameObject = Instantiate(_prefEnemy.gameObject, _spawnPointers[indexSpawnPointer].position, Quaternion.identity);
        TargetFairy enemy = gameObject.GetComponent<TargetFairy>();
        FairyTargets tempFairyTargets = _fairyTargets;
        ControlEnemyFollow(ref fairyType, ref tempFairyTargets);
        enemy.Instantiate(fairyType, tempFairyTargets);
        enemy.OnEnd += () => CheckLiveEnemy(enemy);
        enemy.Play();
    }

    private void ControlEnemyFollow(ref FairyType fairyType, ref FairyTargets tempFairyTargets)
    {
        if (fairyType.IsDolly)
        {
            if (_valueEnemyFollow < _targetsPoints.Count)
            {
                tempFairyTargets.TargetsPoints = new List<Transform>() { _targetsPoints[_valueEnemyFollow] };
                _valueEnemyFollow++;
            }
            else
            {
                fairyType.IsDolly = false;
            }
        }
    }

    private void CheckLiveEnemy(TargetFairy targetFairy)
    {
        targetFairy.OnEnd -= () => CheckLiveEnemy(targetFairy);
        _enemyForWave--;
        if (_enemyForWave <= 0)
        {
            StartWaves();
        }
    }
}

public struct FairyTargets
{
    public Transform FinalTarget;
    public List<Transform> TargetsPoints;
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FairyCreater : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private FairySettings _settings;
    [SerializeField] private Fairy _prefEnemy;
    [Header("SpawnPointers")]
    [SerializeField] private List<Transform> _spawnPointers;
    private List<Fairy> fairies;
    private QuestThree _questThree;
    private FairyTargets _fairyTargets;
    private int _valueEnemyFollow;
    private bool _isPlay;
    public event Action OnCheckOverWaves;
    public event Action OnSleepFairy;

    public void Initialization(FairyTargets fairyTargets, QuestThree questThree)
    {
        _isPlay = true;
        fairies = new();
        _fairyTargets = fairyTargets;
        _questThree = questThree;
    }

    public void SpawnEnemy(int enemyForWave)
    {
        FairyType fairyType;
        for (int i = 0; i < enemyForWave; i++)
        {
            fairyType = _settings.GetEnemyType();
            CreateEnemy(fairyType);
        }
    }

    public FairyItem GetFairyTarget()
    {
        return _questThree.GetFairyTarget();
    }

    public void Stop()
    {
        _isPlay = false;
        for (int i = 0; i < fairies.Count; i++)
        {
            if (fairies[i] != null)
            {
                fairies[i].End();
            }
        }
    }

    private void CreateEnemy(FairyType fairyType)
    {
        int indexSpawnPointer = Random.Range(0, _spawnPointers.Count);
        Fairy enemy = Instantiate(_prefEnemy, _spawnPointers[indexSpawnPointer].position, Quaternion.identity);
        fairies.Add(enemy);
        FairyTargets tempFairyTargets = _fairyTargets;
        ControlEnemyFollow(ref fairyType, ref tempFairyTargets);
        enemy.Instantiate(fairyType, tempFairyTargets, this);
        //enemy.OnEndGame += () => CheckLiveEnemy(enemy);
        enemy.Play();
    }

    private void ControlEnemyFollow(ref FairyType fairyType, ref FairyTargets tempFairyTargets)
    {
        if (_valueEnemyFollow < _fairyTargets.MovePoints.Count)
        {
            tempFairyTargets.MovePoints = new List<Transform>() { _fairyTargets.MovePoints[_valueEnemyFollow] };
            _valueEnemyFollow++;
            fairyType.IsFollow = true;
        }
    }

    public void CheckLiveEnemy(Fairy targetFairy)
    {
        //targetFairy.OnEndGame -= () => CheckLiveEnemy(targetFairy);
        fairies.Remove(targetFairy);
        if (_isPlay)
        {
            OnCheckOverWaves?.Invoke();
        }
        OnSleepFairy?.Invoke();
    }
}
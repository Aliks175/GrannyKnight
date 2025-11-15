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
    private QuestThree _questThree;
    private FairyTargets _fairyTargets;
    private int _valueEnemyFollow;
    public event Action OnCheckOverWaves;

    public void Initialization(FairyTargets fairyTargets, QuestThree questThree)
    {
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

    private void CreateEnemy(FairyType fairyType)
    {
        int indexSpawnPointer = Random.Range(0, _spawnPointers.Count);
        GameObject gameObject = Instantiate(_prefEnemy.gameObject, _spawnPointers[indexSpawnPointer].position, Quaternion.identity);
        Fairy enemy = gameObject.GetComponent<Fairy>();
        FairyTargets tempFairyTargets = _fairyTargets;
        ControlEnemyFollow(ref fairyType, ref tempFairyTargets);
        enemy.Instantiate(fairyType, tempFairyTargets,this);
        enemy.OnEnd += () => CheckLiveEnemy(enemy);
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

    private void CheckLiveEnemy(Fairy targetFairy)
    {
        targetFairy.OnEnd -= () => CheckLiveEnemy(targetFairy);
        OnCheckOverWaves?.Invoke();
    }
}

//public struct FairyTargets
//{
//    public Transform FinalTarget;
//    public List<Transform> MovePoints;
//}
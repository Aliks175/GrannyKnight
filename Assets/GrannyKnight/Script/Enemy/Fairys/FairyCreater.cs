using System.Collections.Generic;
using UnityEngine;

public class FairyCreater : Quest
{
    [Header("Settings")]
    [SerializeField] private FairySettings _settings;
    [SerializeField] private GameObject _prefEnemy;
    [Header("TargetMove")]
    [SerializeField] private List<Transform> _targetsPoint;
    [SerializeField] private Transform _target;
    [Header("SpawnPointers")]
    [SerializeField] private List<Transform> _spawnPointers;
    private bool _isActiveQuest = false;
    private int _wavesCount;

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
        _wavesCount++;
        SpawnEnemy();
        
        //GetValueEnemy();
    }

    private void SpawnEnemy()
    {
      int enemyForWave =  _settings.GetEnemyForWave(_wavesCount);

    }




    private void GetValueEnemy()
    {// Мы получаем настройки из ScriptableObject
     // мы подсчитываем какая это волна фей
     // и по анимационной кривой мы спавним нужное количество фей на волну
     // Феи летают Х количество раз и после летят к дому 

        Debug.Log($"wavesCount {_wavesCount} Value {_settings.GetEnemyForWave(_wavesCount)} ");
    }
}
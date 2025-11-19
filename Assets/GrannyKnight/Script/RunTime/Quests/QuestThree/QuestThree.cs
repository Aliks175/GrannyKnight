using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class QuestThree : Quest
{
    [Header("SubSystem")]
    [SerializeField] private FairyCreater _fairyCreater;
    [SerializeField] private ControlDollyCart _controlDollyCart;
    [SerializeField] private ControlFairyItem _controlFairyItem;
    [SerializeField] private UiThree _uiThree;
    [Header("TargetMove")]
    [SerializeField] private List<Transform> _movePoints;
    [SerializeField] private Transform _finalMoveTarget;
    [Header("QuestSettings")]
    [SerializeField] private AnimationCurve _valueEnemyForWave;
    [SerializeField] private int _valueWaves;
    private int _wavesCount;
    private int _enemyForWave;
    private int _countAllEnemy;
    private int _countTempEnemy;
    private int _countItem;
    private int _countTempItem;
    private bool _isActiveQuest = false;
    public override event Action<QuestEnding> OnEnd;

    private void OnDisable()
    {
        _fairyCreater.OnCheckOverWaves -= CheckOverWaves;
        _controlFairyItem.OnEnd -= () => StopQuest(QuestEnding.Bad);
    }

    private void Start()
    {
        Initialization();
    }

    public void Initialization()
    {
        FairyTargets fairyTargets = new FairyTargets()
        {
            FinalTarget = _finalMoveTarget,
            MovePoints = _movePoints,
        };
        _controlFairyItem.OnEnd += () => StopQuest(QuestEnding.Bad);
        _fairyCreater.OnCheckOverWaves += CheckOverWaves;
        _fairyCreater.Initialization(fairyTargets, this);
        _controlDollyCart.Initialization();
        _controlFairyItem.Initialization();
        ControlUi();
    }

    public override void StartQuest()
    {
        _uiThree.OnUpdateUi(_countTempEnemy, _countTempItem);
        _uiThree.StartTimerGame(StartGame);
    }

    public override void StopQuest(QuestEnding quest)
    {
        OnEnd?.Invoke(quest);
        _controlDollyCart.Stop();
        Debug.Log($"StopQuest - Stop");
        _uiThree.Stop();
        _fairyCreater.Stop();
    }

    public FairyItem GetFairyTarget()
    {
        return _controlFairyItem.GetFairyTarget();
    }

    private void StartGame()
    {
        if (_isActiveQuest) return;
        _controlDollyCart.Play();
        StartWaves();
        _isActiveQuest = true;
    }

    private void StartWaves()
    {
        if (_wavesCount >= _valueWaves)
        {
            EndGame();
            // Проверять если украли хоть 1 то средняя концовка , если украли 0 то хорошая 

            return;
        }
        _fairyCreater.SpawnEnemy(GetEnemyForWave(_wavesCount));
        _wavesCount++;
    }

    private void EndGame()
    {
        if (_countTempItem < _countItem && _countTempItem > 0)
        {
            StopQuest(QuestEnding.Middle);
        }
        else if (_countTempItem <= 0)
        {
            StopQuest(QuestEnding.Bad);
        }
        else if (_countTempItem == _countItem)
        {
            StopQuest(QuestEnding.Good);
        }
    }

    private void CheckOverWaves()
    {
        _enemyForWave--;
        if (_enemyForWave <= 0)
        {
            StartWaves();
        }
    }

    private int GetEnemyForWave(int waveCount)
    {
        _enemyForWave = Mathf.RoundToInt(_valueEnemyForWave.Evaluate(waveCount));
        return _enemyForWave;
    }

    private void ControlUi()
    {
        CountAllEnemy();
        _countItem = _controlFairyItem.CountItem;
        _countTempItem = _countItem;
        _countTempEnemy = _countAllEnemy;
        _uiThree.Initialization(_countAllEnemy, _countItem);
        _controlFairyItem.OnLostItem += (contex) =>
        {
            _countTempItem = contex;
            _uiThree.OnUpdateUi(_countTempEnemy, _countTempItem);
        };
        _fairyCreater.OnSleepFairy += () =>
        {
            _countTempEnemy--;
            _uiThree.OnUpdateUi(_countTempEnemy, _countTempItem);
        };
    }

    private void CountAllEnemy()
    {
        _countAllEnemy = 0;
        for (int i = 0; i < _valueWaves; i++)
        {
            //Debug.Log($"i{i}");
            _countAllEnemy += Mathf.RoundToInt(_valueEnemyForWave.Evaluate(i));
            //Debug.Log($"Mathf.RoundToInt(_valueEnemyForWave.Evaluate(i) {Mathf.RoundToInt(_valueEnemyForWave.Evaluate(i)).ToString()}");
            //Debug.Log($"Temp_allEnemy{_allEnemy}");
        }
        //Debug.Log($"_allEnemy{_allEnemy}");
    }
}
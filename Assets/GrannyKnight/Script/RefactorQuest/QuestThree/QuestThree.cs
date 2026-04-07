using System;
using System.Collections.Generic;
using UnityEngine;

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
    [Header("VictoryCondition")]
    [SerializeField] private VictoryCondition victoryConditions;

    private int _wavesCount;
    private int _enemyForWave;
    private int _countAllEnemy;
    private int _countTempEnemy;
    private int _countItem;
    private int _countTempItem;
    private bool _isActiveQuest = false;
    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;

    private void OnEnable()
    {
        _controlFairyItem.OnEnd += () => StopQuest(QuestEnding.Bad);
        _fairyCreater.OnCheckOverWaves += CheckOverWaves;
        _controlFairyItem.OnLostItem += (contex) =>
        {
            _countTempItem = contex;
            if(_uiThree == null) { return; }
            _uiThree.OnUpdateUi(_countTempEnemy, _countTempItem);
        };
        _fairyCreater.OnSleepFairy += () =>
        {
            _countTempEnemy--;
            if (_uiThree == null) { return; }
            _uiThree.OnUpdateUi(_countTempEnemy, _countTempItem);
        };
    }

    //private void OnDisable()
    //{
    //    _fairyCreater.OnCheckOverWaves -= CheckOverWaves;
    //    _controlFairyItem.OnEnd -= () => StopQuest(QuestEnding.Bad);

    //    _controlFairyItem.OnLostItem -= (contex) =>
    //    {
    //        _countTempItem = contex;
    //        if (_uiThree == null) { return; }
    //        _uiThree.OnUpdateUi(_countTempEnemy, _countTempItem);
    //    };
    //    _fairyCreater.OnSleepFairy -= () =>
    //    {
    //        _countTempEnemy--;
    //        if (_uiThree == null) { return; }
    //        _uiThree.OnUpdateUi(_countTempEnemy, _countTempItem);
    //    };
    //}

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
        _fairyCreater.Initialization(fairyTargets, this);
        _controlDollyCart.Initialization();
        _controlFairyItem.Initialization();
        ControlUi();
    }

    public override void StartQuest()
    {
        OnStart?.Invoke();
        StartGame();
        if (_uiThree == null) { return; }
        _uiThree.OnUpdateUi(_countTempEnemy, _countTempItem);
        _uiThree.StartTimerGame(StartGame);
    }

    public override void StopQuest(QuestEnding quest)
    {
        OnEnd?.Invoke(quest);
        _controlDollyCart.Stop();
        if (_uiThree != null)
        {
        _uiThree.Stop();
        }
        _fairyCreater.Stop();
    }

    public FairyItem GetFairyTarget()
    {
        return _controlFairyItem.GetFairyTarget();
    }

    public void ResetGame()
    {
        _wavesCount = 0;
        _isActiveQuest = false;
        _controlFairyItem.SpawnBlins();
        _controlDollyCart.Initialization();
        _controlDollyCart.Play();
        _controlFairyItem.ResetFairyItem();
        ControlUi();
        OnStart?.Invoke();
        if (_uiThree == null) { return; }
        _uiThree.OnUpdateUi(_countTempEnemy, _countTempItem);
        _uiThree.StartTimerGame(StartGame);
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
            return;
        }
        _fairyCreater.SpawnEnemy(GetEnemyForWave(_wavesCount));
        _wavesCount++;
    }

    private void EndGame()
    {
        if (_countTempItem <= victoryConditions.CountCakeMiddleEnding && _countTempItem > 0)
        {
            StopQuest(QuestEnding.Middle);
            return;
        }
        else if (_countTempItem <= 0)
        {
            StopQuest(QuestEnding.Bad);
            return;
        }
        else if (_countTempItem >= victoryConditions.CountCakeGoodEnding)
        {
            StopQuest(QuestEnding.Good);
            return;
        }
    }

    private void CheckOverWaves()
    {
        Debug.Log($"CheckOverWaves PRE = {_enemyForWave}");
        _enemyForWave--;
        Debug.Log($"CheckOverWaves POST = {_enemyForWave}");
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
        if (_uiThree == null) { return; }
        _uiThree.Initialization(_countAllEnemy, _countItem);

    }

    private void CountAllEnemy()
    {
        _countAllEnemy = 0;
        for (int i = 0; i < _valueWaves; i++)
        {
            _countAllEnemy += Mathf.RoundToInt(_valueEnemyForWave.Evaluate(i));
        }
    }

    [Serializable]
    private struct VictoryCondition
    {
        public int CountCakeGoodEnding;
        public int CountCakeMiddleEnding;
    }
}
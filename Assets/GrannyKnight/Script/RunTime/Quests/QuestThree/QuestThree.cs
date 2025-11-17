using System.Collections.Generic;
using UnityEngine;

public class QuestThree : Quest
{
    [Header("SubSystem")]
    [SerializeField] private FairyCreater _fairyCreater;
    [SerializeField] private ControlDollyCart _controlDollyCart;
    [SerializeField] private ControlFairyItem _controlFairyItem;
    [Header("TargetMove")]
    [SerializeField] private List<Transform> _movePoints;
    [SerializeField] private Transform _finalMoveTarget;
    [Header("QuestSettings")]
    [SerializeField] private AnimationCurve _valueEnemyForWave;
    [SerializeField] private int _valueWaves;

    private int _wavesCount;
    private int _enemyForWave;
    private bool _isActiveQuest = false;

    private void OnDisable()
    {
        _fairyCreater.OnCheckOverWaves -= CheckOverWaves;
    }

    private void Start()
    {
        Initialization();
        _fairyCreater.OnCheckOverWaves += CheckOverWaves;
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
    }

    public override void StartQuest()
    {
        if (_isActiveQuest) return;
        _controlDollyCart.Play();
        StartWaves();
        _isActiveQuest = true;
    }

    public override void StopQuest(QuestEnding quest)
    {
        _controlDollyCart.Stop();
    }

    public FairyItem GetFairyTarget()
    {
        return _controlFairyItem.GetFairyTarget();
    }

 

    private void StartWaves()
    {
        if (_wavesCount >= _valueWaves)
        {
            StopQuest(QuestEnding.Good);
            return;
        }
        _fairyCreater.SpawnEnemy(GetEnemyForWave(_wavesCount));
        _wavesCount++;
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

    
}
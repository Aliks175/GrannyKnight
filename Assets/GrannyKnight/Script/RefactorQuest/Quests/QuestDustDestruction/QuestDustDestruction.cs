using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class QuestDustDestruction : Quest
{
    [Header("Компоненты")]
    [Tooltip("Стадии пыли")][SerializeField] private StageDust[] _stageDust;
    [Tooltip("Префаб пыли")][SerializeField] private TargetDust _prefabDust;
    [Tooltip("Точки старта")][SerializeField] private Transform _spawnPoint;
    [Tooltip("Область создания пыли")][SerializeField] private float _spawnWidth;
    [Tooltip("Игрок")][SerializeField] private Transform _playerTarget;

    [Tooltip("Визуальные частицы при уничтожении")][SerializeField] private GameObject _effectOnDeath;
    [SerializeField] private UiOne _uiOne;

    private List<TargetDust> _dusts = new List<TargetDust>();
    private SystemBuss _systemBuss;
    private FactoryDust _factoryDust;
    private BlackoutScreen _playerStrategy;
    private float _fullHealth = 0f;

    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;

    [Inject]
    public void Construct(SystemBuss systemBuss, FactoryDust factoryDust, BlackoutScreen playerStrategy)
    {
        _systemBuss = systemBuss;
        _factoryDust = factoryDust;
        _playerStrategy = playerStrategy;
    }

    private void OnEnable()
    {
        if (_systemBuss == null) { return; }
        WaitPlayer().Forget();
        _playerStrategy.OnEnd += End;
    }

    public override void StartQuest()
    {
        OnStart?.Invoke();
        _fullHealth = SetHealth();
        StartGame();
        if (_uiOne != null)
        {
            _uiOne.Initialization(_fullHealth);
            _uiOne.StartTimerGame(StartGame);
        }
    }

    public void OnDustDie(TargetDust dust, int stage)
    {
        Instantiate(_effectOnDeath, dust.transform.position, Quaternion.identity, transform);
        //_dusts.Remove(dust);
        if (stage > 0)
        {
            CreateChild(stage, dust.transform);
        }
        if (_dusts.Count == 0)
        {
            StopQuest(QuestEnding.Good);
        }
    }

    // принудительная остановка квеста провал квеста и тд 
    public override void StopQuest(QuestEnding quest)
    {
        //switch (quest)
        //{
        //    case QuestEnding.Good:
        //        Debug.Log("Good endind");
        //        break;
        //    case QuestEnding.Bad:
        //        Debug.Log("Bad endind");
        //        break;
        //    case QuestEnding.Middle:
        //        //Debug.Log("Middle endind");
        //        break;
        //}
        EndGame(quest);
    }

    public void Damage(float damage)
    {
        _fullHealth -= damage;
        if (_uiOne != null)
        {
            _uiOne.OnUpdateUi(_fullHealth);
        }
    }

    private void End()
    {
        StopQuest(QuestEnding.Bad);
        _playerStrategy.OnEnd -= End;
    }

    private void EndGame(QuestEnding quest)
    {
        if (_uiOne != null)
        {
            _uiOne.Stop();
        }

        OnEnd?.Invoke(quest);
        ClearEnemy();
        //_playerTarget.gameObject.GetComponent<PlayerHealthSystem>().Die();
    }

    //private void CheckPlayer()
    //{
    //    PlayerCharacter player = _systemBuss.GetPlayer();
    //    if (player == null)
    //    {
    //        Debug.LogError("Not Found Player");
    //        return;
    //    }
    //    else
    //    {
    //        _playerTarget = player.transform;
    //    }
    //}

    private async UniTaskVoid WaitPlayer()
    {
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        playerCharacter.SetStrategyHealtheble(_playerStrategy);
        _playerTarget = playerCharacter.transform;
    }


    private void ClearEnemy()
    {
        _factoryDust.Dispose();
        //if (_dusts.Count > 0)
        //{
        //    for (int i = 0; i < _dusts.Count; i++)
        //    {
        //        TargetDust temp = _dusts[i];
        //        if (temp != null)
        //        {
        //            Destroy(temp);
        //        }
        //    }
        //    _dusts.Clear();
        //}
    }

    private void StartGame()
    {
        //if (_playerTarget == null) _playerTarget = FindAnyObjectByType<PlayerCharacter>().gameObject.transform;
        TargetDust dust = _factoryDust.GetDust();
        dust.transform.position = _spawnPoint.position;
        dust.SetParameters(_stageDust[_stageDust.Length - 1], this, _playerTarget, _stageDust.Length - 1);
        _dusts.Add(dust);
        //Debug.Log(_fullHealth);
    }



    private void CreateChild(int stage, Transform trans)
    {
        float SpawnWidthStage = _stageDust[stage - 1].SpawnWidthStage;
        float SpawnHeightStage = _stageDust[stage - 1].SpawnHeightStage;
        int childCount = _stageDust[stage].CountChildStage;
        for (int i = 0; i < childCount; i++)
        {
            Vector3 spawnPos = GetRandomPositionInRectangle(trans.position, SpawnWidthStage, SpawnHeightStage);

            TargetDust dust = _factoryDust.GetDust();
            dust.transform.position = spawnPos;
            dust.SetParameters(_stageDust[stage - 1], this, _playerTarget, stage - 1);
            _dusts.Add(dust);
        }
    }

    private Vector3 GetRandomPositionInRectangle(Vector3 center, float width, float height)
    {
        float randomZ = Random.Range(-width / 2f, width / 2f);
        while (Vector3.Distance(center, center + new Vector3(0f, 0, randomZ)) > _spawnWidth)
        {
            randomZ = Random.Range(-width / 2f, width / 2f);
        }
        return center + new Vector3(0f, 0, randomZ);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_spawnPoint.position + new Vector3(0, 0, -_spawnWidth), _spawnPoint.position + new Vector3(0, 0, _spawnWidth));
    }

    private float SetHealth()
    {
        float health = 0f;
        health += _stageDust[_stageDust.Length - 1].HealthStage;
        health += _stageDust[_stageDust.Length - 1].CountChildStage * _stageDust[_stageDust.Length - 2].HealthStage;
        health += _stageDust[_stageDust.Length - 2].CountChildStage * _stageDust[_stageDust.Length - 1].CountChildStage * _stageDust[_stageDust.Length - 3].HealthStage;
        return health;
    }
}
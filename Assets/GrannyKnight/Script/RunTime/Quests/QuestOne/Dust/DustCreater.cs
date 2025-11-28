using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DustCreater : Quest
{
    [Header("Компоненты")]
    [Tooltip("Стадии пыли")][SerializeField] private StageDust[] _stageDust;
    [Tooltip("Префаб пыли")][SerializeField] private GameObject _prefabDust;
    [Tooltip("Точки старта")][SerializeField] private Transform _spawnPoint;
    [Tooltip("Область создания пыли")][SerializeField] private float _spawnWidth;
    [Tooltip("Игрок")][SerializeField] private Transform _playerTarget;

    [Tooltip("Визуальные частицы при уничтожении")][SerializeField] private GameObject _effectOnDeath;
    [SerializeField] private UiOne _uiOne;
    private List<GameObject> _dusts = new List<GameObject>();
    private float _fullHealth = 0f;

    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;

    public override void StartQuest()
    {
        OnStart?.Invoke();
        SetHealth();
        _uiOne.Initialization(_fullHealth);
        _uiOne.StartTimerGame(StartGame);
    }

    public void OnDustDie(Transform dust, int stage)
    {
        Instantiate(_effectOnDeath, dust.position, Quaternion.identity, transform);
        _dusts.Remove(dust.gameObject);
        if (stage > 0)
        {
            CreateChild(stage, dust);
        }
        if (_dusts.Count == 0)
        {
            StopQuest(QuestEnding.Good);
        }
    }

    // принудительная остановка квеста провал квеста и тд 
    public override void StopQuest(QuestEnding quest)
    {
        switch (quest)
        {
            case QuestEnding.Good:
                Debug.Log("Good endind");
                break;
            case QuestEnding.Bad:
                Debug.Log("Bad endind");
                break;
            case QuestEnding.Middle:
                Debug.Log("Middle endind");
                break;
        }
        EndGame(quest);
    }

    public void Damage(float damage)
    {
        _fullHealth -= damage;
        Debug.Log(_fullHealth);
        _uiOne.OnUpdateUi(_fullHealth);
    }

    private void EndGame(QuestEnding quest)
    {
        _uiOne.Stop();
        OnEnd?.Invoke(quest);
        ClearEnemy();
    }

    private void ClearEnemy()
    {
        if (_dusts.Count > 0)
        {
            for (int i = 0; i < _dusts.Count; i++)
            {
                GameObject temp = _dusts[i];
                if (temp != null)
                {
                    Destroy(temp);
                }
            }
            _dusts.Clear();
        }
    }

    private void StartGame()
    {
        if (_playerTarget == null) _playerTarget = FindAnyObjectByType<PlayerCharacter>().gameObject.transform;
        GameObject dust = Instantiate(_prefabDust, _spawnPoint.position, Quaternion.identity, transform);
        dust.GetComponent<TargetDust>().SetParameters(_stageDust[_stageDust.Length - 1], this, _playerTarget, _stageDust.Length - 1);
        _dusts.Add(dust);
        Debug.Log(_fullHealth);
    }

    private void CreateChild(int stage, Transform trans)
    {
        float SpawnWidthStage = _stageDust[stage - 1].SpawnWidthStage;
        float SpawnHeightStage = _stageDust[stage - 1].SpawnHeightStage;
        int childCount = _stageDust[stage].CountChildStage;
        for (int i = 0; i < childCount; i++)
        {
            Vector3 spawnPos = GetRandomPositionInRectangle(trans.position, SpawnWidthStage, SpawnHeightStage);
            GameObject dust = Instantiate(_prefabDust, spawnPos, Quaternion.identity, transform);
            dust.GetComponent<TargetDust>().SetParameters(_stageDust[stage - 1], this, _playerTarget, stage - 1);
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

    private void SetHealth()
    {
        _fullHealth += _stageDust[_stageDust.Length - 1].HealthStage;
        _fullHealth += _stageDust[_stageDust.Length - 1].CountChildStage * _stageDust[_stageDust.Length - 2].HealthStage;
        _fullHealth += _stageDust[_stageDust.Length - 2].CountChildStage * _stageDust[_stageDust.Length - 1].CountChildStage * _stageDust[_stageDust.Length - 3].HealthStage;
    }
}
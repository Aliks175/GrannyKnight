using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
public class DustCreater : Quest
{
    [Header("Компоненты")]
    [Tooltip("Стадии пыли")][SerializeField] private StageDust[] _stageDust;
    [Tooltip("Префаб пыли")][SerializeField] private GameObject _prefabDust;
    [Tooltip("Точки старта")][SerializeField] private Transform _spawnPoint;
    [Tooltip("Область создания пыли")][SerializeField]private float _spawnWidth;
    [Tooltip("Точка конца маршрута")][SerializeField] private Transform _endPoint;
    [Tooltip("Визуальные частицы при уничтожении")][SerializeField] private GameObject _effectOnDeath;
    private List<GameObject> _dusts = new List<GameObject>();

    public override void StartQuest()
    {
        if (_endPoint == null) _endPoint = FindAnyObjectByType<PlayerCharacter>().gameObject.transform;
        GameObject dust = Instantiate(_prefabDust, _spawnPoint.position, Quaternion.identity, transform);
        dust.GetComponent<TargetDust>().SetParameters(_stageDust[_stageDust.Length - 1], this, _endPoint, _stageDust.Length - 1);
        _dusts.Add(dust);
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
                if (_dusts.Count > 0)
                {
                    foreach (GameObject dust in _dusts)
                    {
                        Destroy(dust);
                        _dusts.Remove(dust);
                    }
                }
                Debug.Log("Bad endind");
                break;
            case QuestEnding.Middle:
                Debug.Log("Middle endind");
                break;
        }
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
            dust.GetComponent<TargetDust>().SetParameters(_stageDust[stage - 1], this, _endPoint, stage - 1);
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
        Gizmos.DrawLine(_spawnPoint.position + new Vector3(0,0,-_spawnWidth),_spawnPoint.position + new Vector3(0,0,_spawnWidth));
    }
}
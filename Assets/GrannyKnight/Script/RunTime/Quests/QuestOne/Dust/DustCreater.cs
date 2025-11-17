using UnityEngine;
public class DustCreater : Quest
{
    [Header("Компоненты")]
    [Tooltip("Стадии пыли")][SerializeField] private StageDust[] _stageDust;
    [Tooltip("Префаб пыли")][SerializeField] private GameObject _prefabDust;
    [Tooltip("Точки старта")][SerializeField] private Transform[] _spawnPoint;
    [Tooltip("Точка конца маршрута")][SerializeField] private Transform _endPoint;
    [Tooltip("Визуальные частицы при уничтожении")][SerializeField] private GameObject _effectOnDeath;

    public override void StartQuest()
    {
        GameObject dust = Instantiate(_prefabDust, _spawnPoint[Random.Range(0, _spawnPoint.Length)].position, Quaternion.identity, transform);
        dust.GetComponent<TargetDust>().SetParameters(_stageDust[_stageDust.Length - 1], this, _endPoint, _stageDust.Length - 1);
    }

    public void OnDustDie(Transform dust, int stage)
    {
        Instantiate(_effectOnDeath, dust.position, Quaternion.identity, transform);
        if (stage > 0)
        {
            CreateChild(stage, dust);
        }
    }

    // принудительная остановка квеста провал квеста и тд 
    public override void StopQuest()
    {
        throw new System.NotImplementedException();
    }

    private void CreateChild(int stage, Transform trans)
    {
        float SpawnWidthStage = _stageDust[stage - 1].SpawnWidthStage;
        float SpawnHeightStage = _stageDust[stage - 1].SpawnHeightStage;
        int childCount = _stageDust[stage].CountChildStage;
        for (int i = 0; i < childCount; i++)
        {
            Vector3 spawnPos = GetRandomPositionInRectangle(trans.position, SpawnWidthStage, SpawnHeightStage);
            spawnPos.x = trans.position.x;
            GameObject dust = Instantiate(_prefabDust, spawnPos, Quaternion.identity, transform);
            dust.GetComponent<TargetDust>().SetParameters(_stageDust[stage - 1], this, _endPoint, stage - 1);
        }
    }

    private Vector3 GetRandomPositionInRectangle(Vector3 center, float width, float height)
    {
        float randomY = Random.Range(-height / 2f, height / 2f);
        float randomZ = Random.Range(-width / 2f, width / 2f);

        return center + new Vector3(0f, randomY, randomZ);
    }
}
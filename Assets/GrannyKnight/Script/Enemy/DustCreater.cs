using UnityEngine;
public class DustCreater : MonoBehaviour
{
    [Header("Компоненты")]
    [Tooltip("Стадии пыли")][SerializeField] private StageDust[] _stageDust;
    [Tooltip("Префаб пыли")] [SerializeField]private GameObject _prefabDust;
    [Tooltip("Точки старта")][SerializeField] private Transform[] _spawnPoint;
    [Tooltip("Точка конца маршрута")] [SerializeField] private Transform _endPoint;
    [Tooltip("Визуальные частицы при уничтожении")][SerializeField] private GameObject _effectOnDeath;

    public void CreateOnStart()
    {
        GameObject dust = Instantiate(_prefabDust, _spawnPoint[Random.Range(0, _spawnPoint.Length)].position, Quaternion.identity, transform);
        dust.GetComponent<TargetDust>().SetParameters(_stageDust[_stageDust.Length-1], this, _endPoint);
    }
    void CreateChild(int stage, Transform trans)
    {
        float spawnRadius = _stageDust[stage-1].SpawnRadStage;
        int childCount = _stageDust[stage].CountChildStage;

        for (int i = 0; i < childCount; i++)
        {
            Vector3 spawnPos = GetRandomPositionAround(trans.position, spawnRadius);
            spawnPos.x = trans.position.x;
            
            GameObject dust = Instantiate(_prefabDust, spawnPos, Quaternion.identity, transform);
            dust.GetComponent<TargetDust>().SetParameters(_stageDust[stage - 1], this, _endPoint);
        }
    }

    Vector3 GetRandomPositionAround(Vector3 center, float radius)
    {
        // Случайная точка в сфере
        return center + Random.insideUnitSphere * radius;
    }
    public void OnDustDie(Transform dust, int stage)
    {
        Instantiate(_effectOnDeath, dust.position, Quaternion.identity, transform);
        if (stage > 0)
        {
            CreateChild(stage, dust);
        }

    }

}

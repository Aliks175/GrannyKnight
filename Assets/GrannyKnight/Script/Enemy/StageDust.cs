using UnityEngine;

[CreateAssetMenu(fileName = "StageDust", menuName = "Enemy/StageDust")]
public class StageDust : ScriptableObject
{
    [Tooltip("Порядок пыли, чем выше, тем темнее")][Range(0, 4)]public int Stage;
    [Tooltip("Цвет пыли")]public Color ColorStage;
    [Tooltip("Скорость пыли")]public float SpeedStage;
    [Tooltip("Здоровье пыли")]public float HealthStage;
    [Tooltip("Количество пыли после смерти")] public int CountChildStage;
    [Tooltip("Радиус создания пыли")] public float SpawnRadStage;
    [Tooltip("Минимальный размер пыли")]public float BaseScaleStage;
    [Tooltip("Максимальный размер пыли")] public float MaxScaleStage;
}

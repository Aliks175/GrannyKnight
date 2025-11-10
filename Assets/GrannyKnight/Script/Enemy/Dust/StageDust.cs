using UnityEngine;

[CreateAssetMenu(fileName = "StageDust", menuName = "Create/Enemy/StageDust")]
public class StageDust : ScriptableObject
{
    [Tooltip("Цвет пыли")]public Color ColorStage;
    [Tooltip("Скорость пыли")]public float SpeedStage;
    [Tooltip("Здоровье пыли")]public float HealthStage;
    [Tooltip("Количество пыли после смерти")] public int CountChildStage;
    [Tooltip("Ширина создания пыли")] public float SpawnWidthStage;
    [Tooltip("Высота создания пыли")] public float SpawnHeightStage;
    [Tooltip("Размер пыли")]public float BaseScaleStage;
}

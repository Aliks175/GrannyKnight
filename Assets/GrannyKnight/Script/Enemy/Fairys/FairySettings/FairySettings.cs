using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FairySettings", menuName = "Create/Enemy/FairySettings")]
public class FairySettings : ScriptableObject
{
    [Header("ValueEnemy")]
    [SerializeField] private AnimationCurve _valueEnemyForWave;
    [Header("RangeSpeed")]
    [SerializeField] private List<FairyType> _listFairyTypes;

    public int GetEnemyForWave(int waveCount)
    {
        return Mathf.RoundToInt(_valueEnemyForWave.Evaluate(waveCount));
    }

    public FairyType GetEnemyType()
    {
        if (_listFairyTypes == null)
        {
            Debug.LogError("NotFound -  _listFairyTypes");
        }
        int index = Random.Range(0, _listFairyTypes.Count);
        return _listFairyTypes[index];
    }
}

public struct FairyType
{
    public float Speed;
    public int _minValueChangeTarget;
    public bool IsDolly;
}
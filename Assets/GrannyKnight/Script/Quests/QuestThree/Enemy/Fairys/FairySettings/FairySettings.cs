using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "FairySettings", menuName = "Create/Enemy/FairySettings")]
public class FairySettings : ScriptableObject
{
   
    [Header("RangeSpeed")]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [Header("RangeValueChangeTarget")]
    [SerializeField] private int _minValueChangeTarget;
    [SerializeField] private int _maxValueChangeTarget;
    [Header("RangeTimeWait")]
    [SerializeField] private int _minTimeWait;
    [SerializeField] private int _maxTimeWait;

    public FairyType GetEnemyType()
    {
        int tempValueChangeTarget = Random.Range(_minValueChangeTarget, _maxValueChangeTarget + 1);
        float tempSpeed = Random.Range(_minSpeed, _maxSpeed + 1);
        float tempTime = Random.Range(_minTimeWait, _maxTimeWait + 1);
        bool tempIsFollow = false;
        FairyType fairyType = new FairyType()
        {
            IsFollow = tempIsFollow,
            ValueChangeTarget = tempValueChangeTarget,
            Speed = tempSpeed,
            TimeWait = tempTime
        };
        return fairyType;
    }
}

[Serializable]
public struct FairyType
{
    public float Speed;
    public float TimeWait;
    public int ValueChangeTarget;
    public bool IsFollow;
}
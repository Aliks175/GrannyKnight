using UnityEngine;

[CreateAssetMenu(fileName = "Condition", menuName = "Create/Quest/Condition")]
public class CheckWeaponCondition : ConditionQuest
{
    public override float Evaluate()
    {
        return 1f;
    }
}
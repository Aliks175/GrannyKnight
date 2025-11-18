using UnityEngine;

[CreateAssetMenu(fileName = "CheckWeapon", menuName = "Create/Quest/Condition/CheckWeapon")]
public class CheckWeaponCondition : ConditionQuest
{
    [SerializeField] private TypeWeapon TypeWeapon;
    public override bool Evaluate(IPlayerDatable playerDatable)
    {
        return playerDatable.ChooseWeapon.FindWeapon(TypeWeapon);
    }
}
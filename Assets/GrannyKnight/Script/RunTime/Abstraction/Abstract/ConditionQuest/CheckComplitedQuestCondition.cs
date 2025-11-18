using UnityEngine;

[CreateAssetMenu(fileName = "CheckComplitedQuest", menuName = "Create/Quest/Condition/CheckComplited")]
public class CheckComplitedQuestCondition : ConditionQuest
{
    [Header("CheckComplitedNextQuest")]
    [SerializeField] private ConditionQuest[] _idQuests;

    public override bool Evaluate(IPlayerDatable playerDatable)
    {
        foreach (var item in _idQuests)
        {
            if (!item.Evaluate(playerDatable))
            {
                return false;
            }
        }
        return true;
    }
}

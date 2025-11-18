using UnityEngine;

[CreateAssetMenu(fileName = "QuestViewSettings", menuName = "Create/Quest/View")]
public class QuestViewSettings : ScriptableObject
{
    public QuestInfo QuestInfo;
    public ConditionQuest Conditions;

    public bool CheckCompliteQuest(IPlayerDatable playerDatable)
    {
        bool is—ompleted = true;
        if (Conditions != null)
        {
            is—ompleted = Conditions.Evaluate(playerDatable);
        }
        return is—ompleted;
    }
}
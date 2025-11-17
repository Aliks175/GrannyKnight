using UnityEngine;

[CreateAssetMenu(fileName = "QuestViewSettings", menuName = "Create/Quest/View")]
public class QuestViewSettings : ScriptableObject
{
    public QuestInfo questInfo;
    public ConditionQuest conditions;

    public void CheckCompliteQuest()
    {

    }
}

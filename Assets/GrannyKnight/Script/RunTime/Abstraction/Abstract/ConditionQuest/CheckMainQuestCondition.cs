using UnityEngine;

[CreateAssetMenu(fileName = "CheckMainQuest", menuName = "Create/Quest/Condition/CheckMainQuest")]
public class CheckMainQuestCondition : ConditionQuest
{
    [SerializeField] private int _indexMainQuest;

    public override bool Evaluate(IPlayerDatable playerDatable)
    {
        bool isComplited = false;
        switch (_indexMainQuest)
        {
            case 1:

                isComplited = playerDatable.PlayerQuestControl.MainQuestList.OneQuestEnding != QuestEnding.None;

                Debug.Log($"MainQuestList.OneQuestEnding = {playerDatable.PlayerQuestControl.MainQuestList.OneQuestEnding}");
                Debug.Log($"OneQuestEnding != QuestEnding.None; = {isComplited}");
                break;
            case 2:
                isComplited = playerDatable.PlayerQuestControl.MainQuestList.TwoQuestEnding != QuestEnding.None;
                break;
            case 3:
                isComplited = playerDatable.PlayerQuestControl.MainQuestList.ThreeQuestEnding != QuestEnding.None;
                break;
            default:
                break;
        }
        return isComplited;
    }
}

using UnityEngine;

public class QuestGetter : MonoBehaviour
{
    [SerializeField] private QuestViewSettings _questInfo;
    [SerializeField] private PlayerQuestControl _playerQuestControl;

    public void GetQuest()
    {
        _playerQuestControl.SetQuest(_questInfo);
    }
}
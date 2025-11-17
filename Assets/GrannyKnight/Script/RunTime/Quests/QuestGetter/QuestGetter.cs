using UnityEngine;

public class QuestGetter : MonoBehaviour
{
    [SerializeField] private QuestViewControl _questViewControl;
    [SerializeField] private QuestViewSettings _questInfo;

    public void GetQuest()
    {
        _questViewControl.SetQuest(_questInfo);
    }
}
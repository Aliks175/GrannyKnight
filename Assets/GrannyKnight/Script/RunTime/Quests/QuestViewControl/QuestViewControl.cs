using TMPro;
using UnityEngine;

public class QuestViewControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameQuest;
    [SerializeField] private TextMeshProUGUI _descriptionQuest;

    public void SetInfo(QuestInfo questInfo)
    {
        _nameQuest.SetText(questInfo.NameQuest);
        _descriptionQuest.SetText(questInfo.Description);
    }
}

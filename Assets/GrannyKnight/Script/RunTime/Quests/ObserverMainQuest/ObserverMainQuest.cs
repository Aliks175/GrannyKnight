using UnityEngine;

public class ObserverMainQuest : MonoBehaviour
{
    [SerializeField] private Quest quest;
    [SerializeField] private int _indexMainQuest;
    [SerializeField] private PlayerCharacter character;

    private void OnEnable()
    {
        quest.OnEnd += OverMainQuest;
    }

    private void OnDisable()
    {
        quest.OnEnd -= OverMainQuest;
    }

    private void OverMainQuest(QuestEnding quest)
    {
        Debug.Log(new string('-', 10));
        Debug.Log($"End Vers = {quest}");
        character.PlayerData.PlayerQuestControl.OverMainQuest(_indexMainQuest, quest);
    }
}

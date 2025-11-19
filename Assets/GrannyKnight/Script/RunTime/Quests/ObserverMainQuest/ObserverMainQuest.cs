using UnityEngine;
using UnityEngine.Events;

public class ObserverMainQuest : MonoBehaviour
{
    [SerializeField] private Quest quest;
    [SerializeField] private int _indexMainQuest;
    [SerializeField] private PlayerCharacter character;

    public UnityEvent OnEnd;
    public UnityEvent OnEndGood;
    public UnityEvent OnEndMidle;
    public UnityEvent OnEndBad;


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
        ActiveEvent(quest);
    }

    private void ActiveEvent(QuestEnding quest)
    {
        switch (quest)
        {
            case QuestEnding.Good:
                OnEndGood?.Invoke();
                break;
            case QuestEnding.Bad:
                OnEndBad?.Invoke();
                break;
            case QuestEnding.Middle:
                OnEndMidle?.Invoke();
                break;
            default:
                break;
        }
        OnEnd?.Invoke();
    }
}

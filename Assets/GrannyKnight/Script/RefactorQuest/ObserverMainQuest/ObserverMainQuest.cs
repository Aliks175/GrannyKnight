using UnityEngine;
using UnityEngine.Events;

public class ObserverMainQuest : MonoBehaviour
{
    [SerializeField] private Quest quest;

    public UnityEvent OnStart;
    public UnityEvent OnEnd;
    public UnityEvent OnEndGood;
    public UnityEvent OnEndMidle;
    public UnityEvent OnEndBad;

    private void OnEnable()
    {
        quest.OnStart += OnStartQuest;
        quest.OnEnd += OverMainQuest;
    }

    private void OnDisable()
    {
        quest.OnStart -= OnStartQuest;
        quest.OnEnd -= OverMainQuest;
    }

    private void OnStartQuest()
    {
        OnStart?.Invoke();
    }

    private void OverMainQuest(QuestEnding quest)
    {
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
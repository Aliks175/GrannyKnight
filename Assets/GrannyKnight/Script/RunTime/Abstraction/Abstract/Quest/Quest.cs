using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public abstract void StartQuest();
    public abstract void StopQuest(QuestEnding quest);
}
public enum QuestEnding
{
    Good,
    Bad,
    Middle
}

using System;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public abstract void StartQuest();
    public abstract void StopQuest(QuestEnding quest);

    public abstract event Action<QuestEnding> OnEnd;
}

public enum QuestEnding
{
    None,
    Good,
    Bad,
    Middle
}

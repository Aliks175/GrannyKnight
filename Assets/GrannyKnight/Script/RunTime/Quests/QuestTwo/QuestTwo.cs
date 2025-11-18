using System;

public class QuestTwo : Quest
{
    public override event Action<QuestEnding> OnEnd;

    public override void StartQuest()
    {

    }

    public override void StopQuest(QuestEnding quest)
    {
        OnEnd?.Invoke(quest);
    }
}

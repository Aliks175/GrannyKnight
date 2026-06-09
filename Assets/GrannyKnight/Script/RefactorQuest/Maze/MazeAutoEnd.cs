using System;
using UnityEngine;

public class MazeAutoEnd : Quest
{
    [SerializeField] private float _delay;

    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;


    public void StopTimer()
    {
        CancelInvoke(nameof(OnAutoEnd));
        OnAutoEnd();
    }

    public override void StartQuest()
    {
        OnStart?.Invoke();
        Invoke(nameof(OnAutoEnd), _delay);
        Debug.Log("MazeAutoEnd - StartQuest");
    }

    public void CloseMaze()
    {
        StopQuest(QuestEnding.Good);
    }

    public override void StopQuest(QuestEnding quest)
    {
        CancelInvoke(nameof(OnAutoEnd));
        OnEnd?.Invoke(quest);
    }
    private void OnAutoEnd()
    {
        StopQuest(QuestEnding.Bad);
    }
}
using System;
using System.Collections.Generic;

public class ControlBubbles
{
    private List<TargetBubble> _targets;
    private int _goalCountBubblesDestruction;
    private int _countBubblesDestruction;

    public event Action OnStopQuest;

    public ControlBubbles()
    {
        _targets = new();
    }

    public void AddBubbles(TargetBubble targetBubble)
    {
        targetBubble.gameObject.SetActive(false);
        _targets.Add(targetBubble);
    }

    public void AddCountBubblesDestruction()
    {
        _countBubblesDestruction++;
        if (_countBubblesDestruction >= _goalCountBubblesDestruction)
        {
            StopQuest();
        }
    }

    public void StartQuest(int countBubblesDestruction)
    {
        _goalCountBubblesDestruction = countBubblesDestruction;
        foreach (var targetBubble in _targets)
        {
            targetBubble.gameObject.SetActive(true);
        }
    }

    public void StopQuest()
    {
        OnStopQuest?.Invoke();
        foreach (var targetBubble in _targets)
        {
            targetBubble.gameObject.SetActive(false);
        }
    }
}

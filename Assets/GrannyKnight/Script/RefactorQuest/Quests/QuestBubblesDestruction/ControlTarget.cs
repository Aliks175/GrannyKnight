using System;
using System.Collections.Generic;

public class ControlTarget
{
    private List<ITarget> _targets;
    private int _goalCountTargetDestruction;
    private int _countTargetDestruction;

    public event Action OnStopQuest;

    public ControlTarget()
    {
        _targets = new();
    }

    public void AddTarget(ITarget targetBubble)
    {
        targetBubble.Body.SetActive(false);
        _targets.Add(targetBubble);
    }

    public void AddCountTargetDestruction()
    {
        _countTargetDestruction++;
        if (_countTargetDestruction >= _goalCountTargetDestruction)
        {
            StopQuest();
        }
    }

    public void StartQuest(int countBubblesDestruction)
    {
        _goalCountTargetDestruction = countBubblesDestruction;
        foreach (var targetBubble in _targets)
        {
            targetBubble.Body.SetActive(true);
        }
    }

    public void StopQuest()
    {
        OnStopQuest?.Invoke();
        foreach (var targetBubble in _targets)
        {
            targetBubble.Body.SetActive(false);
        }
    }
}
using System;
using System.Collections.Generic;

public class ControlTarget
{
    private int _goalCountTargetDestruction;
    private int _countTargetDestruction;

    public event Action OnStartQuest;
    public event Action OnStopQuest;

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
        OnStartQuest?.Invoke();
    }

    public void StopQuest()
    {
        OnStopQuest?.Invoke();
    }
}
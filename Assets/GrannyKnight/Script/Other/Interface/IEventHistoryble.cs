using System;

public interface IEventHistoryble
{
    public event Action<int> OnActiveHistory;
}
using System;
using Zenject;

public class ImporterBussEventHistory : IDisposable, IInitializable
{
    private IEventHistoryble interactebleEventHistory;
    private SystemBuss _systemBuss;
    private HistoryManager _historyManager;

    public ImporterBussEventHistory(SystemBuss systemBuss, HistoryManager historyManager)
    {
        _systemBuss = systemBuss;
        _historyManager = historyManager;
    }

    public void Dispose()
    {
        _systemBuss.OnEventHistory -= EventHistory;
        if (interactebleEventHistory != null)
        {
            interactebleEventHistory.OnActiveHistory -= OnActiveHistory;
        }
    }

    public void Initialize()
    {
        _systemBuss.OnEventHistory += EventHistory;
    }

    private void EventHistory(IEventHistoryble history)
    {
        interactebleEventHistory = history;
        interactebleEventHistory.OnActiveHistory += OnActiveHistory;
    }

    private void OnActiveHistory(int id, IEventHistoryble eventHistoryble)
    {
        eventHistoryble.OnActiveHistory -= OnActiveHistory;
        _historyManager.ActiveHistory(id);
    }
}

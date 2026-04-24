using UnityEngine;
using Zenject;

public class HistoryInstaller : MonoInstaller
{
    [SerializeField] private HistoryManager _historyManager;

    public override void InstallBindings()
    {
        BindHistory();
        BindImporter();
    }

    private void BindHistory()
    {
        Container.Bind<HistoryManager>()
           .FromInstance(_historyManager)
           .AsSingle();
    }


    private void BindImporter()
    {
        Container.BindInterfacesAndSelfTo<ImporterBussEventHistory>()
            .AsSingle()
            .NonLazy();
    }
}
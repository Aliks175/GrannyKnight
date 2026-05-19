using UnityEngine;
using Zenject;

public class SecondFloorInstaller : MonoInstaller
{
    [SerializeField] private DragQuest _dragObject;

    public override void InstallBindings()
    {
        BindQuestDrag();
    }

    private void BindQuestDrag()
    {
        Container.Bind<DragQuest>()
           .FromInstance(_dragObject)
           .AsSingle()
           .NonLazy();
    }
}
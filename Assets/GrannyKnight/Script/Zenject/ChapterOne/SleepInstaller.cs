using UnityEngine;
using Zenject;

public class SleepInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindQuestBubblesDestruction();
    }

    private void BindQuestBubblesDestruction()
    {
        Container.Bind<ControlBubbles>()
           .AsSingle();
    }
}
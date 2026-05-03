using Zenject;
using UnityEngine;

public class DialogueSystemInstaller : MonoInstaller
{
    [SerializeField] private DialogueCanvas dialogueUI;

    public override void InstallBindings()
    {
        // Localization
        Container.Bind<LocalizationManager>().AsSingle();

        // Dialogue
        Container.Bind<DialogueCanvas>().FromInstance(dialogueUI).AsSingle();
        Container.BindInterfacesAndSelfTo<DialogueManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<DialogueSystemInstalizer>().AsSingle();
    }
}
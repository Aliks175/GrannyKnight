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
        Container.Bind<DialogueManager>().AsSingle();

        Container.BindInterfacesTo<DialogueSystemInstalizer>().AsSingle();
        Container.Bind<DialogueCanvas>().FromInstance(dialogueUI).AsSingle();
    }
}
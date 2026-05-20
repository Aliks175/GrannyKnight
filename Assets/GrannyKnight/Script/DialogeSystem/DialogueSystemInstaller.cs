using UnityEngine;
using Zenject;

public class DialogueSystemInstaller : MonoInstaller
{
    [SerializeField] private string _pathLocalization = "Localization/ru";
    public override void InstallBindings()
    {
        BindLocalization();
        BindDialogue();
        BindImporter();
    }

    private void BindLocalization()
    {
        Container.Bind<LocalizationManager>().AsTransient();
    }

    private void BindDialogue()
    {
        Container.BindInterfacesAndSelfTo<DialogueManager>()
            .AsSingle()
            .WithArguments(_pathLocalization);
           
        //Container.BindInterfacesAndSelfTo<DialogueSystemInstalizer>().AsSingle();
    }

    private void BindImporter()
    {
        Container.BindInterfacesAndSelfTo<DialogueManagerCanvasImporter>().AsSingle();
    }
}
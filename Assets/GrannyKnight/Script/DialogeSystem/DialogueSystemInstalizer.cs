using Zenject;

public class DialogueSystemInstalizer : IInitializable
{
    private readonly LocalizationManager localization;

    public DialogueSystemInstalizer(LocalizationManager localization)
    {
        this.localization = localization;
    }

    public void Initialize()
    {
        localization.Load("Localization/ru");
    }
}

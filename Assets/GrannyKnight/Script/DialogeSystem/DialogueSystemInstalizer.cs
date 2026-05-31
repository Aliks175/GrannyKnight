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
        // Больше не нужно загружать общий файл локализации
        // Файлы локализации будут загружаться автоматически при запуске диалогов
    }
}

using System;
using Zenject;

public class ImporterGameManagerLoading : IDisposable, IInitializable
{
    private LoadingScreen _loadingScreen;
    private GameManager _gameManager;

    public ImporterGameManagerLoading(LoadingScreen loadingScreen, GameManager gameManager)
    {
        _loadingScreen = loadingScreen;
        _gameManager = gameManager;
    }

    public void Dispose()
    {
        _gameManager.OnLoad += OnLoad;
    }

    public void Initialize()
    {
        _gameManager.OnLoad += OnLoad;
    }

    private void OnLoad(object sender, OnProgressLoading progress)
    {
        _loadingScreen.ChangeProgressBar(progress.SceneProgress);
    }
}

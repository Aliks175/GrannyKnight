using System;
using UnityEngine;
using Zenject;

public class ControlBoot : MonoBehaviour
{
    private LoadingScreen _loadingScreen;
    private GameManager _gameManager;

    [Inject]
    private void Construct(GameManager gameManager, LoadingScreen loadingScreen)
    {
        _gameManager = gameManager;
        _loadingScreen = loadingScreen;
    }
    
    private void OnDisable()
    {
        _gameManager.OnLoad -= OnLoad;
    }

    private void OnEnable()
    {
        _gameManager.OnLoad += OnLoad;
    }

    private void OnLoad(object sender, OnProgressLoading progress)
    {
        _loadingScreen.ChangeProgressBar(progress.SceneProgress);
    }


    //private void OnLoad(bool isload)
    //{
    //    _loadingScreen.gameObject.SetActive(isload);
    //}

    private void Awake()
    {
        _gameManager.Bootload();
    }
}

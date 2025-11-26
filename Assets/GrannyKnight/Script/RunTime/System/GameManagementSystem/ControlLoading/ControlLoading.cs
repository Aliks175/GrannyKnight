using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlLoading : MonoBehaviour
{
    //[SerializeField] private ListScene listScene;

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync((int)ListScene.Game, LoadSceneMode.Single);
    }

    public void LoadTestGame()
    {
        SceneManager.LoadSceneAsync((int)ListScene.TestGameBild, LoadSceneMode.Single);
    }

    public void LoadTestGameGamePlay()
    {
        SceneManager.LoadSceneAsync((int)ListScene.GameGamePlay, LoadSceneMode.Single);
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync((int)ListScene.Menu, LoadSceneMode.Single);
    }
}

public enum ListScene
{
    Menu = 0,
    Game = 1,
    GameGamePlay = 2,
    TestGameBild = 3,
}
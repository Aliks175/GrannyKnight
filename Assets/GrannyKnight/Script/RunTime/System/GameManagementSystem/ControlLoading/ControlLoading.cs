using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlLoading : MonoBehaviour
{
    [SerializeField] private ListScene listScene;
    //[SerializeField] private GameObject _panelLoading;
    //[SerializeField] private Slider _loadSlider;
    //private List<AsyncOperation> asyncOperations = new List<AsyncOperation>();

    //private void Awake()
    //{
    //    asyncOperations.Add(SceneManager.LoadSceneAsync((int)ListScene.Menu, LoadSceneMode.Additive));
    //}

    public void Initialization()
    {
        //_panelLoading.SetActive(true);
        // asyncOperations.Add(  Инициализация
        // здесь мы должны взять сохранения игры из вк если они есть и инициировать в игре
        //StartCoroutine(GetSceneLoadProgress(InitializeMenu));
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync((int)listScene, LoadSceneMode.Single);
        //_panelLoading.SetActive(true);
        //asyncOperations.Clear();
        //asyncOperations.Add(SceneManager.UnloadSceneAsync((int)ListScene.Menu));
        //asyncOperations.Add(SceneManager.LoadSceneAsync((int)ListScene.Game, LoadSceneMode.Additive));
        //StartCoroutine(GetSceneLoadProgress(InitializeGame));
    }



    //public void LoadMenu()
    //{
    //    _panelLoading.SetActive(true);
    //    asyncOperations.Clear();
    //    asyncOperations.Add(SceneManager.UnloadSceneAsync((int)ListScene.Game));
    //    asyncOperations.Add(SceneManager.LoadSceneAsync((int)ListScene.Menu, LoadSceneMode.Additive));
    //    //StartCoroutine(GetSceneLoadProgress(InitializeMenu));
    //}

    //private void InitializeGame()
    //{
    //    SetUpGame setUpGame = GameObject.FindFirstObjectByType<SetUpGame>();
    //    setUpGame.Initialization();
    //}

    //private void InitializeMenu()
    //{
    //    //SetUpMenu setUpMenu = GameObject.FindFirstObjectByType<SetUpMenu>();
    //    //setUpMenu.Initialize();
    //}

    //private IEnumerator GetSceneLoadProgress(Action action)
    //{
    //    for (int i = 0; i < asyncOperations.Count; i++)
    //    {
    //        while (!asyncOperations[i].isDone)
    //        {
    //            yield return null;
    //        }
    //    }
    //    action?.Invoke();
    //    _panelLoading.SetActive(false);
    //}
}

public enum ListScene
{
    Menu = 0,
    Null = 1,
    Game = 2,
    TestGameBild = 3,
}
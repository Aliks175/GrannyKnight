using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class GameManager : IDisposable, IInitializable
{
    public AsyncOperation CurrentOperation => _currentOperation;
    private Loading _loading;
    private List<AsyncOperation> scenesLoading;
    private CancellationTokenSource _cancellationToken;
    private OnProgressLoading _progressLoading;
    private AsyncOperation _currentOperation;
    private float _totalSceneProgress;

    public event EventHandler<OnProgressLoading> OnLoad;

    public GameManager(Loading testLoading)
    {
        _loading = testLoading;
        scenesLoading = new();
        _progressLoading = new();
    }

    public void Initialize()
    {
        _cancellationToken?.Dispose();
        _cancellationToken = new CancellationTokenSource();
    }

    public void Dispose()
    {
        _cancellationToken?.Cancel();
        _cancellationToken?.Dispose();
    }

    public void Bootload()
    {
        //OnLoad?.Invoke(true);
        _loading.LoadAdditive(ListScene.Menu);
    }


    public void AddScene(ListScene listScene)
    {
       var tempScene = _loading.LoadAdditive(listScene);
        tempScene.allowSceneActivation = false;
        _currentOperation = tempScene;
    }

    public void RemoveScene(ListScene listScene)
    {
        _loading.UnLoadAdditive(listScene);
    }

    public void LoadGame()
    {
        CheckAsyncOperation(_loading.LoadSingle(ListScene.GamePlay));
        CheckAsyncOperation(_loading.LoadAdditive(ListScene.RoomPlayer));
        StartTimer(_cancellationToken.Token).Forget();
    }

    public void LoadMenu()
    {
        scenesLoading.Clear();
        CheckAsyncOperation(_loading.LoadSingle(ListScene.Menu));
        StartTimer(_cancellationToken.Token).Forget();
    }

    //public void LoadFreeGame()
    //{
    //    _loading.UnLoadAdditive(ListScene.Game);
    //    _loading.UnLoadAdditive(ListScene.Menu);
    //    //_loading.LoadAdditive(ListScene.FreeGame);
    //}

    private async UniTaskVoid StartTimer(CancellationToken token)
    {
        try
        {
            _totalSceneProgress = 0;
            for (int i = 0; i < scenesLoading.Count; i++)
            {
                while (!scenesLoading[i].isDone)
                {
                    foreach (AsyncOperation operation in scenesLoading)
                    {
                        _totalSceneProgress += operation.progress;
                    }
                    _totalSceneProgress = _totalSceneProgress / scenesLoading.Count;
                    _progressLoading.SceneProgress = _totalSceneProgress;
                    _progressLoading.IsLoading = true;
                    OnLoad?.Invoke(this, _progressLoading);
                    await UniTask.Yield(PlayerLoopTiming.Update);
                }
            }
            //Debug.Log($"StartTimer = {_totalSceneProgress}");
            _progressLoading.SceneProgress = Mathf.CeilToInt(_totalSceneProgress);
            _progressLoading.IsLoading = false;
            OnLoad?.Invoke(this, _progressLoading);
            EndLoading();
        }
        catch (OperationCanceledException)
        {
            EndLoading();
            Debug.Log("Операция отменена ");
        }
    }

    private void EndLoading()
    {
        scenesLoading.Clear();
    }

    private void CheckAsyncOperation(AsyncOperation asyncOperation)
    {
        if (asyncOperation == null) return;
        scenesLoading.Add(asyncOperation);
    }
}

public class OnProgressLoading : EventArgs
{
    public float SceneProgress;
    public bool IsLoading;
}
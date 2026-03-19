using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class GameManager : IDisposable, IInitializable
{
    private Loading _loading;
    private List<AsyncOperation> scenesLoading;
    private CancellationTokenSource _cancellationToken;
    private float _totalSceneProgress;
    private OnProgressLoading _progressLoading;

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

    public void LoadGame()
    {
        //scenesLoading.Add(_loading.UnLoadAdditive(ListScene.Menu));
        //scenesLoading.Add(_loading.LoadAdditive(ListScene.Game));

        CheckAsyncOperation(_loading.UnLoadAdditive(ListScene.Menu));
        CheckAsyncOperation(_loading.LoadAdditive(ListScene.Game));

        StartTimer(_cancellationToken.Token).Forget();
    }

    public void LoadMenu()
    {
        CheckAsyncOperation(_loading.UnLoadAdditive(ListScene.Game));
        CheckAsyncOperation(_loading.LoadAdditive(ListScene.Menu));


        StartTimer(_cancellationToken.Token).Forget();
    }

    public void LoadFreeGame()
    {
        _loading.UnLoadAdditive(ListScene.Game);
        _loading.UnLoadAdditive(ListScene.Menu);
        _loading.LoadAdditive(ListScene.FreeGame);
    }

    private async UniTaskVoid StartTimer(CancellationToken token)
    {
        try
        {
            for (int i = 0; i < scenesLoading.Count; i++)
            {
                while (!scenesLoading[i].isDone)
                {
                    //token.ThrowIfCancellationRequested();

                    _totalSceneProgress = 0;

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
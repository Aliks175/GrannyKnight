using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class QuestTwo : Quest
{
    public static QuestTwo Instance { get; private set; }
    public override event Action<QuestEnding> OnEnd;
    [SerializeField] private GameObject _basket;
    [SerializeField] private float _timeToSleep = 5f;
    [SerializeField] private Transform _startPos;
    private float _time;
    private int _fruitCount = 0;
    private Tween _tween, _tweenToStart;
    private CancellationTokenSource _cancellationTokenSource;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _basket.transform.position = _startPos.position;
        Instance = this;
    }
    public Vector3 BasketPos => _basket.transform.position;

    public override void StartQuest()
    {

    }
    public void CollectFruit(Vector3 pos, float time)
    {
        _cancellationTokenSource?.Cancel();
        _tweenToStart.Kill();
        _tween = _basket.transform.DOMove(pos, time);
        _tween.Play();
    }
    public void IsCollected()
    {
        _fruitCount++;
        _time = Time.time + _timeToSleep;
        Debug.Log(_fruitCount);
        TimeToSleep().Forget();
    }
    private async UniTaskVoid TimeToSleep()
    {
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
        await UniTask.WaitUntil(() => Time.time >= _time, cancellationToken: _cancellationTokenSource.Token);
        ToStartPoint();
    }
    private void ToStartPoint()
    {
        _tweenToStart = _basket.transform.DOMove(_startPos.position, _timeToSleep);
        _tweenToStart.Play();
    }

    public override void StopQuest(QuestEnding quest)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;
        OnEnd?.Invoke(quest);
    }
}

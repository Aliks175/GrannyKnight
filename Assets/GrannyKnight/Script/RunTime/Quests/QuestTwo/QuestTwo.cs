using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class QuestTwo : Quest
{
    
    // Синглтон с ленивой инициализацией
    public static QuestTwo Instance { get; private set; }
    
    // События
    public override event Action<QuestEnding> OnEnd;
    
    // Сериализуемые поля
    [Header("Quest Settings")]
    [SerializeField] private GameObject _basket;
    [SerializeField] private float _timeToSleep = 5f;
    [SerializeField] private Transform _startPos;
    [SerializeField] private float _movementDurationToBack = 1f;
    [SerializeField] private int[] _targetFruit;
    [SerializeField] private float _timeToQuest;
    
    // Приватные поля
    private int _fruitCount = 0;
    private float _timer;
    private float _sleepEndTime;
    private Tween _currentTween;
    private CancellationTokenSource _cts;
    private bool _isQuestActive;

    #region Unity Lifecycle
    
    private void Awake()
    {
        InitializeSingleton();
        ResetBasketPosition();
    }
    
    private void OnDestroy()
    {
        Cleanup();
    }
    
    #endregion

    #region Public API
    
    public Vector3 BasketPos => _basket.transform.position;
    
    public override void StartQuest()
    {
        _isQuestActive = true;
        ResetQuestState();
        StartTimer().Forget();
    }
    
    public void CollectFruit(Vector3 fruitPosition, float time)
    {
        if (!_isQuestActive) return;
        CancelCurrentOperations();
        MoveBasketToFruit(fruitPosition, time);
    }
    
    public void OnFruitCollected()
    {
        if (!_isQuestActive) return;
        
        _fruitCount++;
        UpdateSleepTimer();
        
        Debug.Log($"Fruits collected: {_fruitCount}");
        
       
        StartSleepTimer();
    }
    
    public override void StopQuest(QuestEnding questEnding)
    {
        _isQuestActive = false;
        Cleanup();
        OnEnd?.Invoke(questEnding);
    }
    private async UniTaskVoid StartTimer()
    {
        _timer = _timeToQuest;
        while (_timer > 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _timer--;
        }
        CompleteQuest();
    }
    
    #endregion

    #region Private Methods
    
    private void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }
    
    private void ResetBasketPosition()
    {
        if (_basket != null && _startPos != null)
        {
            _basket.transform.position = _startPos.position;
        }
    }
    
    private void ResetQuestState()
    {
        _fruitCount = 0;
        ResetBasketPosition();
        Cleanup();
    }
    
    private void CancelCurrentOperations()
    {
        _cts?.Cancel();
        _currentTween?.Kill();
        _cts = null;
    }
    
    private void MoveBasketToFruit(Vector3 fruitPosition, float time)
    {
        _currentTween = _basket.transform
            .DOMove(fruitPosition, time)
            .SetEase(Ease.OutCubic)
            .OnComplete(OnBasketArrivedAtFruit);
        _currentTween.Play();
    }
    
    private void OnBasketArrivedAtFruit()
    {
        // Можно добавить логику при прибытии корзины
        Debug.Log("Basket arrived at fruit");
    }
    
    private void UpdateSleepTimer()
    {
        _sleepEndTime = Time.time + _timeToSleep;
    }
    
    private void StartSleepTimer()
    {
        CancelCurrentOperations();
        
        _cts = new CancellationTokenSource();
        WaitForSleepPeriod().Forget();
    }
    
    private async UniTaskVoid WaitForSleepPeriod()
    {
        try
        {
            await UniTask.WaitUntil(
                () => Time.time >= _sleepEndTime, 
                cancellationToken: _cts.Token
            );
            
            ReturnBasketToStart();
        }
        catch (OperationCanceledException)
        {
            // Таймер был отменен - это нормально
        }
    }
    
    private void ReturnBasketToStart()
    {
        if (_startPos == null) return;
        
        _currentTween = _basket.transform
            .DOMove(_startPos.position, _movementDurationToBack)
            .SetEase(Ease.InOutCubic);
    }
    
    private void CompleteQuest()
    {
        if (_fruitCount <= _targetFruit[0])
        {
            StopQuest(QuestEnding.Bad);
        }
        else if (_fruitCount <= _targetFruit[1])
        {
            StopQuest(QuestEnding.Middle);
        }
        else if (_fruitCount >= _targetFruit[2])
        {
            StopQuest(QuestEnding.Good);
        }
    }
    
    private void Cleanup()
    {
        CancelCurrentOperations();
        _cts?.Dispose();
        _cts = null;
    }
    
    #endregion
}
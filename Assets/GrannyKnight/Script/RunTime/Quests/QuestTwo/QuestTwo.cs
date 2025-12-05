using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

public class QuestTwo : Quest
{
    // Синглтон с ленивой инициализацией
    public static QuestTwo Instance { get; private set; }

    // События
    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;

    [Header("SubSystem")]
    [SerializeField] private UiTwo _uiTwo;

    // Сериализуемые поля
    [Header("Quest Settings")]
    [SerializeField] private GameObject _basket;
    [SerializeField] private Transform _startPos;
    [SerializeField] private float _timeToSleep = 5f;
    [SerializeField] private float _movementDurationToBack = 1f;
    [SerializeField] private float _timeToQuest;
    [SerializeField] private int[] _targetFruit;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private GameObject[] _fruits;

    // Приватные поля
    private Tween _currentTween;
    private FruitData[] _originalFruitData;
    private CancellationTokenSource _cts;

    [System.Serializable]
    private struct FruitData
    {
        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        public Transform parent;
    }
    private float _timer;
    private float _sleepEndTime;
    private int _maxCountFruit;
    private int _fruitCount = 0;
    private bool _isQuestActive;

    #region Unity Lifecycle

    private void Awake()
    {
        Initialization();
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
        OnStart?.Invoke();
        RestoreFruits();
        _fruitCount = 0;
        _uiTwo.ResetUi();
        _uiTwo.StartTimerGame(StartGame);
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
        _uiTwo.OnUpdateUiProgress(_fruitCount);
        UpdateSleepTimer();

        //Debug.Log($"Fruits collected: {_fruitCount}");

        CheckWin();
        StartSleepTimer();
    }

    public override void StopQuest(QuestEnding questEnding)
    {
        _isQuestActive = false;
        Cleanup();
        OnEnd?.Invoke(questEnding);
    }

    public void ParticleStart(Vector3 pos)
    {
        _particle.transform.position = pos;
        _particle.Play();
    }    

    #endregion

    #region Private Methods

    private void CheckWin()
    {
        if(_fruitCount >= _targetFruit[2])
        {
            _timer = 0;
        }
    }

    private void StartGame()
    {
        _isQuestActive = true;
        ResetQuestState();
        StartTimer().Forget();
    }

    private void EndGame()
    {
        _uiTwo.Stop();
    }

    private async UniTaskVoid StartTimer()
    {
        _timer = _timeToQuest;
        while (_timer > 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _timer--;
            _uiTwo.OnUpdateUiTimer(_timer);
        }
        CompleteQuest();
    }

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
        //Debug.Log("Basket arrived at fruit");
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
        else if (_fruitCount <= _targetFruit[1] && _fruitCount > _targetFruit[0])
        {
            StopQuest(QuestEnding.Middle);
        }
        else if (_fruitCount >= _targetFruit[2])
        {
            StopQuest(QuestEnding.Good);
        }
        EndGame();
    }

    private void Cleanup()
    {
        CancelCurrentOperations();
        _cts?.Dispose();
        _cts = null;
    }

    private void Initialization()
    {
        InitializeSingleton();
        ResetBasketPosition();
        _timer = _timeToQuest;
        _fruitCount = 0;
        CalculateMaxCountFruit();
        SaveOriginalFruits();
        _uiTwo.Initialization(_timer, _maxCountFruit);
    }

    private void CalculateMaxCountFruit()
    {
        _maxCountFruit = 0;
        if (_targetFruit == null) return;
        _maxCountFruit = _targetFruit[2];
    }
    private void SaveOriginalFruits()
    {
        if (_originalFruitData == null)
        {
            _originalFruitData = new FruitData[_fruits.Length];
            for (int i = 0; i < _fruits.Length; i++)
            {
                GameObject copy = Instantiate(_fruits[i], _fruits[i].transform.parent);
                copy.SetActive(false);
                _originalFruitData[i] = new FruitData
                {
                    prefab = copy,
                    position = _fruits[i].transform.position,
                    rotation = _fruits[i].transform.rotation,
                    parent = _fruits[i].transform.parent
                };
            }
        }
    }

    private void RestoreFruits()
    {
        for (int i = 0; i < _originalFruitData.Length; i++)
        {
            if (_fruits[i] == null)
            {
                _fruits[i] = Instantiate(_originalFruitData[i].prefab, _originalFruitData[i].position, _originalFruitData[i].rotation, _originalFruitData[i].parent);
                _fruits[i].SetActive(true);
            }
        }
    }

    #endregion
}
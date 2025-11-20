using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class QuestTwo : Quest
{
    public static QuestTwo Instance { get; private set; }
    public override event Action<QuestEnding> OnEnd;
    [SerializeField] private GameObject _basketCreate;
    [SerializeField] private GameObject _basket;
    [SerializeField] private float _timeToCollect = 3f;
    [SerializeField] private Vector3 _basketPos;
    private float _time;
    private int _fruitCount = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public override void StartQuest()
    {

    }
    public void CollectFruit(Vector3 pos)
    {
        _basket.SetActive(false);
        _time = _timeToCollect +Time.time;
        Timer().Forget();
        Instantiate(_basketCreate, pos + _basketPos, Quaternion.identity);
    }
    public void IsCollected()
    {
        _fruitCount++;
        Debug.Log(_fruitCount);
    }
    private async UniTaskVoid Timer()
    {
        await UniTask.WaitUntil(() => Time.time >= _time);
        _basket.SetActive(true);
    }

    public override void StopQuest(QuestEnding quest)
    {
        OnEnd?.Invoke(quest);
    }
}

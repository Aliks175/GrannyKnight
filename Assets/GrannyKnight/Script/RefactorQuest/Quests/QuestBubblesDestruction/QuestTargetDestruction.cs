using System;
using UnityEngine;
using Zenject;

public class QuestTargetDestruction : Quest
{
    // Квест суть игроку нужно порозить Х мешеней на локации
    // каждая уничтоженная мишень двигает счетчик как только игрок
    // уничтожает нужное количество то срабатывает тригер конца игры
    // в ней нет разных концовок 


    // Нужно создать класс целей это 2Д спрайты при поподании они уничтожаются увеличивая счетчик
    // в нем через Zenject проинициирован класс ControlBubbles это не моно класс который создается при инициации сцены
    // 

    [SerializeField] private int _countBubblesDestruction;
    private ControlTarget _controlBubbles;

    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;

    [Inject]
    public void Construct(ControlTarget controlBubbles)
    {
        _controlBubbles = controlBubbles;
    }

    private void OnDisable()
    {
        _controlBubbles.OnStopQuest -= OnStopQuest;
    }

    private void Start()
    {
        _controlBubbles.OnStopQuest += OnStopQuest;
    }

    public override void StartQuest()
    {
        OnStart?.Invoke();
        _controlBubbles.StartQuest(_countBubblesDestruction);
    }

    public override void StopQuest(QuestEnding quest)
    {
        OnEnd?.Invoke(quest);
    }

    private void OnStopQuest()
    {
        StopQuest(QuestEnding.None);
    }
}
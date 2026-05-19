using System;
using Unity.Cinemachine;
using UnityEngine;

public class DragQuest : Quest
{
    [SerializeField] private GameObject _cameraQuest;
    [Header("Настройка рецептов")]
    public Recipe RecipeWater;
    public Recipe RecipePot;
    public Recipe RecipeMortar;
    public Recipe RecipeFinal;
    public int NumberMortar;
    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;

    public override void StartQuest()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _cameraQuest.SetActive(true);
        OnStart?.Invoke();
    }

    public override void StopQuest(QuestEnding quest)
    {
        _cameraQuest.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnEnd?.Invoke(quest);
    }
}
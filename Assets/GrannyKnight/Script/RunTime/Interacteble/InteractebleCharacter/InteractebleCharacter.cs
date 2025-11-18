
using UnityEngine;

public class InteractebleCharacter : Interacteble
{
    [SerializeField] private QuestChecker _questChecker;
    [SerializeField] private QuestGetter _questGetter;

    protected override void Interact()
    {
        _questGetter.GetQuest();
        _questChecker.CheckQuestIsComplited();
    }
}
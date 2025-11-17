using DG.Tweening;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class QuestViewControl : MonoBehaviour
{
    [SerializeField] private CanvasGroup _questPanel;
    [SerializeField] private GameObject _poolQuest;
    [SerializeField] private QuestView _questPref;

    private List<QuestView> questViews;
    private Tween _tweenOpen;
    private Tween _tweenClose;

    private void Start()
    {
        questViews = new();
        _tweenOpen = DOTween.To(() => _questPanel.alpha, x => _questPanel.alpha = x, 1f, 0.2f);
        _tweenClose = DOTween.To(() => _questPanel.alpha, x => _questPanel.alpha = x, 0f, 0.2f);
    }

    public void ShowQuestPanel()
    {
        _tweenOpen.Play();
        Debug.Log("ShowQuest");
    }

    public void HideQuestPanel()
    {
        _tweenClose.Play();
        Debug.Log("HideQuest");
    }

    public void SetQuest(QuestViewSettings questInfo)
    {
        QuestView newQuest = Instantiate(_questPref, _poolQuest.transform);
        newQuest.SetQuest(questInfo);
        newQuest.ShowQuestPanel();
        questViews.Add(newQuest);
        newQuest.OnDestroy += ClearQuest;
    }

    public void OverQuest(int id)
    {
        QuestView tempQuest =  GetQuest(id);
        if (tempQuest != null)
        {
            tempQuest.HideQuestPanel();
        }
    }

    public void StartQuest(int id)
    {
        QuestView tempQuest = GetQuest(id);
        if (tempQuest != null)
        {
            tempQuest.HideQuestPanel();
        }
    }

    public QuestView GetQuest(int id)
    {
        QuestView tempQuest = null;
        foreach (var quest in questViews)
        {
            if (quest.ID == id)
            {
                tempQuest = quest;
                return tempQuest;
            }
        }
        return tempQuest;
    }

    private void ClearQuest(QuestView questView)
    {
        questView.OnDestroy -= ClearQuest;
        questViews.Remove(questView);
    }

}
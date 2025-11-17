using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    public int ID => _questId;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _discriptionText;
    [SerializeField] private CanvasGroup _questViewPanel;
    private QuestViewSettings _questViewSettings;
    private Tween _tweenOpen;
    private Tween _tweenClose;
    private int _questId;
    public event Action<QuestView> OnDestroy;

    private void OnDisable()
    {
        OnDestroy?.Invoke(this);
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
        _questViewSettings = questInfo;
        _nameText.SetText(_questViewSettings.questInfo.NameQuest);
        _discriptionText.SetText(_questViewSettings.questInfo.Description);
        _questId = _questViewSettings.questInfo.Id; 
        Initialization();
    }

    private void Initialization()
    {
        _tweenOpen = DOTween.To(() => _questViewPanel.alpha, x => _questViewPanel.alpha = x, 1f, 0.5f);
        _tweenClose = DOTween.To(() => _questViewPanel.alpha, x => _questViewPanel.alpha = x, 0f, 0.5f).OnComplete(() => Destroy(gameObject));
    }
}

[Serializable]
public struct QuestInfo
{
    public string NameQuest;
    public string Description;
    public int Id;
}
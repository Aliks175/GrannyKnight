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
    private QuestInfo _questInfo;
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

    public void SetQuest(QuestInfo questInfo)
    {
        _questInfo = questInfo;
        _nameText.SetText(_questInfo.NameQuest);
        _discriptionText.SetText(questInfo.Description);
        _questId = questInfo.Id; 
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
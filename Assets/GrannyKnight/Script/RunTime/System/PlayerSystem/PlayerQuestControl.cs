using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestControl : MonoBehaviour
{
    public QuestList MainQuestList => _mainQuestList;
    [SerializeField]private QuestList _mainQuestList;
    [SerializeField] private QuestViewControl _questViewControl;
    private List<QuestData> _listQuestData;
    private IPlayerDatable _playerDatable;

    public void Initialization(IPlayerDatable playerDatable)
    {
        _mainQuestList = new QuestList();
        _questViewControl.Initialization();
        _playerDatable = playerDatable;
        _listQuestData = new();
    }

    public void SetQuest(QuestViewSettings questViewSettings)
    {
        QuestData tempQuestData = new(questViewSettings);
        if (GetQuestData(questViewSettings.QuestInfo.Id) == null)
        {
            _listQuestData.Add(tempQuestData);
            _questViewControl.SetQuest(tempQuestData.GetQuestInfo());
        }
    }

    public bool GetCheckOverQuest(int idQuest)
    {
        bool isComplited = false;
        QuestData data = GetQuestData(idQuest);
        if (data != null)
        {
            isComplited = data.CheckCompliteQuest(_playerDatable);
            return isComplited;
        }
        return isComplited;
    }

    public void CheckAllQuestForOver()
    {
        for (int i = 0; i < _listQuestData.Count; i++)
        {
            if (_listQuestData[i].CheckCompliteQuest(_playerDatable))
            {
                QuestView questView = _questViewControl.GetQuest(_listQuestData[i].GetQuestInfo().Id);
                if (questView != null)
                {
                    questView.HideQuestPanel();
                }
            }
        }
    }

    public void OverMainQuest(int indexMainQuest, QuestEnding quest)
    {
        switch (indexMainQuest)
        {
            case 1:
                _mainQuestList.OneQuestEnding = quest;
                break;
            case 2:
                _mainQuestList.TwoQuestEnding = quest;
                break;
            case 3:
                _mainQuestList.ThreeQuestEnding = quest;
                break;
            default:
                break;
        }
    }

    private QuestData GetQuestData(int idQuest)
    {
        QuestData data = null;
        for (int i = 0; i < _listQuestData.Count; i++)
        {
            if (_listQuestData[i].GetQuestInfo().Id == idQuest)
            {
                data = _listQuestData[i];
                return data;
            }
        }
        return data;
    }
}

[Serializable]
public struct QuestList
{
    public QuestEnding OneQuestEnding;
    public QuestEnding TwoQuestEnding;
    public QuestEnding ThreeQuestEnding;
}
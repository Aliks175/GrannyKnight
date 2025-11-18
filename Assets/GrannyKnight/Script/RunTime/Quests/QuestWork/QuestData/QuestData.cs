
public class QuestData
{
    public bool IsComplited => _isComplited;

    private QuestViewSettings _questViewSetting;
    private QuestEnding _questEnding;
    private bool _isComplited;

    public QuestData(QuestViewSettings QuestViewSetting)
    {
        _questViewSetting = QuestViewSetting;
        _isComplited = false;
    }

    public bool CheckCompliteQuest(IPlayerDatable playerDatable)
    {
        _isComplited = _questViewSetting.CheckCompliteQuest(playerDatable);
        return _isComplited;
    }

    //public void OverQuest()

    public QuestInfo GetQuestInfo()
    {
       return _questViewSetting.QuestInfo;
    }
}
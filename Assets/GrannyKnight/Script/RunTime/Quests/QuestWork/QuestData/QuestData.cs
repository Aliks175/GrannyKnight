
public class QuestData
{
    private QuestInfo _questInfo;

    public QuestData(QuestViewSettings QuestViewSetting)
    {
        _questInfo = QuestViewSetting.QuestInfo;
    }

    public QuestInfo GetQuestInfo()
    {
        return _questInfo;
    }
}
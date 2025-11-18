using UnityEngine;
using UnityEngine.Events;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private PlayerQuestControl _playerQuestControl;
    [SerializeField] private int _idQuest;

    public UnityEvent OnCompliteQuest;
    public UnityEvent OnNotCompliteQuest;

    public void CheckQuestIsComplited()
    {
        _playerQuestControl.CheckAllQuestForOver();
        bool isComplited = _playerQuestControl.GetCheckOverQuest(_idQuest);
        if (isComplited)
        {
            OnCompliteQuest?.Invoke();
        }
        else
        {
            OnNotCompliteQuest?.Invoke();
        }
    }
}
using System;
using UnityEngine;

public class QuestView : MonoBehaviour
{

    // Здесь должен быть класс который будет висеть на панеле UI через него можно будет
    // изменить имя квеста и его условие отображение
    // мы создаем его на QuestCanvas
    // а на тригерах меняем текс 
    // Для мини игр мы спавним свой UI

    [SerializeField] private QuestViewControl _questViewControl;
    [SerializeField] private QuestInfo _questInfo;

    public void SetQuest()
    {
        _questViewControl.SetInfo(_questInfo);
    }
}

[Serializable]
public struct QuestInfo
{
    public string NameQuest;
    public string Description;
}
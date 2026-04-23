using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PasswordButton : Interacteble
{
    [SerializeField] private int _number;
    [SerializeField] private UnityEvent _toBase;
    public bool IsActive = true;
    private QuestPasswordSelection _quest;

    [Inject]
    public void Construct(QuestPasswordSelection quest)
    {
        _quest = quest;
        _quest.RegisterButton(this);
    }
    public void ResetState()
    {
        _toBase.Invoke();
    }
    public override void BaseInteract()
    {
        if (!IsActive) return;
        base.BaseInteract();
        _quest.SetElement(_number);
    }
}

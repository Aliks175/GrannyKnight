using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PasswordButton : Interacteble
{
    [SerializeField] private int _number;
    private QuestPasswordSelection _quest;

    [Inject]
    public void Construct(QuestPasswordSelection quest)
    {
        _quest = quest;
        _quest.RegisterButton(this);
    }
    public void ResetState(ButtonPasswordState state)
    {
        switch (state)
        {
            case ButtonPasswordState.Base:
                break;
            case ButtonPasswordState.Lose:
                break;
            case ButtonPasswordState.Win:
                break;
        }
    }
    public override void BaseInteract()
    {
        _quest.SetElement(_number);
    }
    public enum ButtonPasswordState
    {
        Base,
        Lose,
        Win
    }
}

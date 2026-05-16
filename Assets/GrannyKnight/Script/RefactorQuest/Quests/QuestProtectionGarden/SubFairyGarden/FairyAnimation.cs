using UnityEngine;
using Zenject;

public class FairyAnimation: IInitializable
{
    private const string _onDead = "Dead";
    private  int _iDAnimationDead;

    public void Initialize()
    {
        _iDAnimationDead = Animator.StringToHash(_onDead);
    }

    public void OnDead(Animator animator)
    {
        animator.SetTrigger(_iDAnimationDead);
    }

}

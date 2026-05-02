using UnityEngine;
using Zenject;

public class TargetBubble : MonoBehaviour, IHealtheble, ITarget
{
    private ControlTarget _controlTarget;

    public GameObject Body => gameObject;

    [Inject]
    public void Construct(ControlTarget controlBubbles)
    {
        _controlTarget = controlBubbles;
        _controlTarget.AddBubbles(this);
    }

    public void TakeDamage(float damage)
    {
        _controlTarget.AddCountTargetDestruction();
        gameObject.SetActive(false);
    }
}
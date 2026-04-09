using UnityEngine;
using Zenject;

public class TargetBubble : MonoBehaviour, IHealtheble
{
    private ControlBubbles _controlBubbles;

    [Inject]
    public void Construct(ControlBubbles controlBubbles)
    {
        _controlBubbles = controlBubbles;
    }

    private void Start()
    {
        _controlBubbles.AddBubbles(this);
    }

    public void TakeDamage(float damage)
    {
        _controlBubbles.AddCountBubblesDestruction();
        gameObject.SetActive(false);
    }
}
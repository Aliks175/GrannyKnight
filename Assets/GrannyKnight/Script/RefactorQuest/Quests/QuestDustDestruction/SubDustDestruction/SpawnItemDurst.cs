using UnityEngine;
using Zenject;

public class SpawnItemDurst : MonoBehaviour
{
    [SerializeField] private ItemDurstPool _prefPoolDurstItem;
    private TargetItemDurst[] _targets;
    private ControlTarget _controlTarget;

    [Inject]
    public void Construct(ControlTarget controlBubbles)
    {
        _controlTarget = controlBubbles;
    }

    public void Spawn()
    {
        ItemDurstPool poolBubles = GameObject.Instantiate(_prefPoolDurstItem, transform);
        Initialization(poolBubles);
    }

    private void Initialization(ItemDurstPool poolBubles)
    {
        _targets = poolBubles.TargetsItemDurst;
        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i].Construct(_controlTarget);
        }
    }
}
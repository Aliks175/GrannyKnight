using UnityEngine;

public class ItemDurstPool : MonoBehaviour
{
    public TargetItemDurst[] TargetsItemDurst => _targets;
    [SerializeField] private TargetItemDurst[] _targets;
}

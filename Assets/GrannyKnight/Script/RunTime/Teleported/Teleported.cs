using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class Teleported : MonoBehaviour
{
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private Transform _positionTeleported;
    [SerializeField] private BlackOut _blackOut;

    public void ActiveTeleport()
    {
        playerCharacter.transform.position = _positionTeleported.position;
        _blackOut.Active();
    }
}
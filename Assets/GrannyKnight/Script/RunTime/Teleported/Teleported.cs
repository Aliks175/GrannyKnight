using UnityEngine;

public class Teleported : MonoBehaviour
{
    [SerializeField] private CharacterController playerCharacter;
    [SerializeField] private Transform _positionTeleported;
    [SerializeField] private BlackOut _blackOut;

    public void ActiveTeleport()
    {
        playerCharacter.transform.position = _positionTeleported.position;
        _blackOut.Active();
    }
}
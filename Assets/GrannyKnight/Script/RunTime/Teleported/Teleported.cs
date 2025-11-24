using UnityEngine;

public class Teleported : MonoBehaviour
{
    [SerializeField] private CharacterController playerCharacter;
    [SerializeField] private Transform _positionTeleported;
    [SerializeField] private BlackOut _blackOut;

    private void Start()
    {
        playerCharacter = GameObject.FindFirstObjectByType<CharacterController>();
    }

    public void ActiveTeleport()
    {
        if (playerCharacter == null) return;
        _blackOut.Active();
        playerCharacter.enabled = false;
        playerCharacter.transform.position = _positionTeleported.position;
        playerCharacter.enabled = true;
    }
}
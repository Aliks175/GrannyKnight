using UnityEngine;

public class Teleported : MonoBehaviour
{
    [SerializeField] private CharacterController _playerCharacter;
    [SerializeField] private Transform _positionTeleported;
    [SerializeField] private BlackOut _blackOut;

    private void Start()
    {
        _blackOut = GameObject.FindFirstObjectByType<BlackOut>();
        _playerCharacter = GameObject.FindFirstObjectByType<CharacterController>();
    }

    public void ActiveTeleport()
    {
        if (_playerCharacter == null) return;
        _blackOut.Active();
        _playerCharacter.enabled = false;
        _playerCharacter.transform.position = _positionTeleported.position;
        _playerCharacter.enabled = true;
    }
}
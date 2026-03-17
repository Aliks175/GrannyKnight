using UnityEngine;

public class Teleported : MonoBehaviour
{
    [SerializeField] private CharacterController _playerCharacter;
    [SerializeField] private Transform _positionTeleported;

    private void Start()
    {
        _playerCharacter = GameObject.FindFirstObjectByType<CharacterController>();
    }

    public void ActiveTeleport()
    {
        if (_playerCharacter == null) return;
        _playerCharacter.enabled = false;
        _playerCharacter.transform.position = _positionTeleported.position;
        _playerCharacter.enabled = true;
    }
}
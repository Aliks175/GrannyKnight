using Unity.Cinemachine;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public IPlayerDatable PlayerData => _playerData;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerLook _playerLook;
    [SerializeField] private PlayerAim _playerAim;
    [SerializeField] private PlayerInteracteble _playerInteracteble;
    [SerializeField] private PlayerControlAnimation _playerControlAnimation;
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    private IPlayerDatable _playerData;

    public void Initialization( Transform head)
    {
        _playerData = GetComponent<IPlayerDatable>();
        SetUp(head, _cinemachineCamera);
    }

    private void SetUp( Transform head, CinemachineCamera cinemachineCamera)
    {
        _playerControlAnimation.Initialization();
        _playerMover.Initialization(_playerControlAnimation);
        _playerLook.Initialization();
        _playerAim.Initialization(cinemachineCamera);
        _playerInteracteble.Initialization(head);
        _playerData.Initialization(this);
    }
}
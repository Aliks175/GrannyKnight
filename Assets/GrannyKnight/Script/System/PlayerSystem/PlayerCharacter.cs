using Unity.Cinemachine;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public IPlayerDatable PlayerData => _playerData;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerLook _playerLook;
    [SerializeField] private PlayerAim _playerAim;
    [SerializeField] private PlayerInteracteble _playerInteracteble;
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    private IPlayerDatable _playerData;

    public void Initialization(Camera camera)
    {
        _playerData = GetComponent<IPlayerDatable>();
        SetUp(camera, _cinemachineCamera);
    }

    private void SetUp(Camera camera, CinemachineCamera cinemachineCamera)
    {
        _playerMover.Initialization();
        _playerLook.Initialization();
        _playerAim.Initialization(cinemachineCamera);
        _playerInteracteble.Initialization(camera);
        _playerData.Initialization(this, camera);
    }
}
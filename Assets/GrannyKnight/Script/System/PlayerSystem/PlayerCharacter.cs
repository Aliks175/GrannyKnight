using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public IPlayerDatable PlayerData => _playerData;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerLook _playerLook;
    [SerializeField] private PlayerAim _playerAim;
    [SerializeField] private PlayerInteracteble _playerInteracteble;
    private IPlayerDatable _playerData;

    public void Initialization(Camera camera)
    {
        _playerData = GetComponent<IPlayerDatable>();
        SetUp(camera);
    }

    private void SetUp(Camera camera)
    {
        _playerMover.Initialization();
        _playerLook.Initialization();
        _playerAim.Initialization(camera);
        _playerInteracteble.Initialization(camera);
        _playerData.Initialization(this, camera);
    }
}
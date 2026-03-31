using UnityEngine;
using Zenject;

public class Teleported : MonoBehaviour
{
    [SerializeField] private Transform _positionTeleported;
    private PlayerCharacter _player;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    public void ActiveTeleport()
    {
        if (CheckPlayer()) { return; }
        
        _player.gameObject.SetActive(false);
        _player.transform.position = _positionTeleported.position;
        _player.gameObject.SetActive(true);
    }


    private bool CheckPlayer()
    {
        if (_player == null) 
        {
            _player = _systemBuss.GetPlayer();
        }

        return _player == null;
    }
}
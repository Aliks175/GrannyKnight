using UnityEngine;

public class PickUp : Interacteble
{
    [Header("Item")]
    [SerializeField] private BazeItem _bazeItem;
    [SerializeField] private TypeUse _isDestroyForOver;
    [Header("Player")]
    [SerializeField] private LayerMask _layerPlayer;
    [SerializeField] private float _distanceForPlayer = 5;
    private PlayerCharacter _playerCharacter;

    protected override void Interact()
    {
        if (_playerCharacter == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceForPlayer, _layerPlayer);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out PlayerCharacter playerCharacter))
                {
                    _playerCharacter = playerCharacter;
                    break;
                }
            }
        }
        if (_playerCharacter == null) return;
        if (_playerCharacter.PlayerData.Inventory.CheckSlot(_bazeItem))
        {
            if (_isDestroyForOver == TypeUse.Expendable)
            {
                Destroy(gameObject);
            }
        }
    }
}
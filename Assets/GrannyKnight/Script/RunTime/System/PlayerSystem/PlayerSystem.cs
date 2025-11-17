using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    private PlayerCharacter _playerCharacter;

    public void Initialization(PlayerCharacter playerCharacter, Transform head)
    {
        _playerCharacter = playerCharacter;
        _playerCharacter.Initialization(head);
    }
}
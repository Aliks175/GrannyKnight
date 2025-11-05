using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    private PlayerCharacter _playerCharacter;

    public void Initialization(PlayerCharacter playerCharacter, Camera camera)
    {
        _playerCharacter = playerCharacter;
        _playerCharacter.Initialization(camera);
    }
}
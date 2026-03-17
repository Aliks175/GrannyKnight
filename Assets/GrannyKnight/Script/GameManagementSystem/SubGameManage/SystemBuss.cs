using System;

public class SystemBuss
{
    private PlayerCharacter player;

    public event Action<PlayerUi> OnConstructPlayerUi;
    public event Action<PlayerCharacter> OnSpawnPlayer;

    public void ConstructPlayerUi(PlayerUi playerUi)
    {
        OnConstructPlayerUi?.Invoke(playerUi);
    }

    public void SpawnPlayer(PlayerCharacter playerCharacter)
    {
        player = playerCharacter;
        OnSpawnPlayer?.Invoke(playerCharacter);
    }

    public PlayerCharacter GetPlayer()
    {
        return player;
    }
}
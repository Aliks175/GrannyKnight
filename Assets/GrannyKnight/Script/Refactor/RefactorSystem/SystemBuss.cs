using System;

public class SystemBuss
{
    public event Action<PlayerUi> OnConstructPlayerUi;

    public void ConstructPlayerUi(PlayerUi playerUi)
    {
        OnConstructPlayerUi?.Invoke(playerUi);
    }
}
using System;

public class PasswordButton : Interacteble
{
    public event Action OnInteract;

    public override void BaseInteract()
    {
        OnInteract?.Invoke();
    }
}
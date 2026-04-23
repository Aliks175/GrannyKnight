public class PlayerHealth
{
    private IPlayerStrategyHealtheble _playerHealtheble;

    public void SetStrategyHealtheble(IPlayerStrategyHealtheble strategy)
    {
        _playerHealtheble = strategy;
    }

    public void TakeDamage(int damage)
    {
        if (_playerHealtheble == null) { return; }
        _playerHealtheble.TakeDamage(damage);
    }

    //public void SetHealth(int health)
    //{
    //    if (_playerHealtheble == null) { return; }
    //    _playerHealtheble.SetHealth(health);
    //}
}
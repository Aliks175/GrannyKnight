public class PlayerHealth
{
    private IPlayerStrategyHealtheble _playerHealtheble;
    private bool _isBlockActive;

    public void SetStrategyHealtheble(IPlayerStrategyHealtheble strategy)
    {
        _playerHealtheble = strategy;
    }

    public void TakeDamage(float damage)
    {
        if (_playerHealtheble == null) { return; }
        if (_isBlockActive) { return; }
        _playerHealtheble.TakeDamage(damage);
    }

    public void Block(bool isActive)
    {
        _isBlockActive = isActive;
    }

    //public void SetHealth(int health)
    //{
    //    if (_playerHealtheble == null) { return; }
    //    _playerHealtheble.SetHealth(health);
    //}
}
using Zenject;

public class TestPlayerSetUpUi : IInitializable
{
    private FactoryPlayerUi _factoryPlayerUi;

    public TestPlayerSetUpUi(FactoryPlayerUi factoryPlayerUi)
    {
        _factoryPlayerUi = factoryPlayerUi;
    }

    public void Initialize()
    {
        _factoryPlayerUi.CreatePlayerUi();
        _factoryPlayerUi.CreateGameUi();
    }
}
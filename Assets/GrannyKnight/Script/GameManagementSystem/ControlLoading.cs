using UnityEngine;
using Zenject;

public class ControlLoading : MonoBehaviour
{
    private Loading _testLoading;

    [Inject]
    public void Construct(Loading testLoading)
    {
        _testLoading = testLoading;
    }

    public void LoadGame()
    {
        _testLoading.LoadSingle(ListScene.Game);
    }

    public void LoadMenu()
    {
        _testLoading.LoadSingle(ListScene.Menu);
    }

    public void LoadFreeGame()
    {
        _testLoading.LoadSingle(ListScene.FreeGame);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
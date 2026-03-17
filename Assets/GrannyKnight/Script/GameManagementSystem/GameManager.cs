using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ListScene _listScene;
    private Loading _testLoading;

    [Inject]
    private void Construct(Loading testLoading)
    {
        _testLoading = testLoading;
    }

    private void Awake()
    {
        _testLoading.LoadAdditive(_listScene);
    }
}
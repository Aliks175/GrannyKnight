using UnityEngine;
using Zenject;

public class TestGameManager : MonoBehaviour
{
    [SerializeField] private ListScene _listScene;
    private TestLoading _testLoading;

    [Inject]
    private void Construct(TestLoading testLoading)
    {
        _testLoading = testLoading;
    }

    private void Awake()
    {
        _testLoading.LoadAdditive(_listScene);
    }
}
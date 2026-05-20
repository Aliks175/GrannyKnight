using UnityEngine;
using Zenject;

public class AddScene : MonoBehaviour
{
    [SerializeField] private ListScene listScene;
    private ControlLoading _controlLoading;

    [Inject]
    private void Construct(ControlLoading controlLoading)
    {
        _controlLoading = controlLoading;
    }

    public void Active()
    {
        _controlLoading.AddScene(listScene);
    }
}

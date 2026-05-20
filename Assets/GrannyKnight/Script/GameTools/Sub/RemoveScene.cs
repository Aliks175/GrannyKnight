using UnityEngine;
using Zenject;

public class RemoveScene : MonoBehaviour
{
    [SerializeField] private ListScene listScene;
    private ControlLoading _controlLoading;

    [Inject]
    public void Construct(ControlLoading controlLoading)
    {
        _controlLoading = controlLoading;
    }

    public void Active()
    {
        _controlLoading.RemoveScene(listScene);
    }
}
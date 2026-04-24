using UnityEngine;
using Zenject;

public class InteractebleRemoveScene : Interacteble
{
    [SerializeField] private ListScene listScene;
    private ControlLoading _controlLoading;

    [Inject]
    public void Construct(ControlLoading controlLoading)
    {
        _controlLoading = controlLoading;
    }

    public override void BaseInteract()
    {
        base.BaseInteract();
        _controlLoading.RemoveScene(listScene);
    }
}

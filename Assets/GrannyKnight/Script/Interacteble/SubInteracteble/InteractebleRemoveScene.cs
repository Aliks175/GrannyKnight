using UnityEngine;
using Zenject;

public class InteractebleRemoveScene : Interacteble
{
    [SerializeField] private ListScene listScene;
    private GameManager _controlLoading;

    [Inject]
    public void Construct(GameManager controlLoading)
    {
        _controlLoading = controlLoading;
    }

    public override void BaseInteract()
    {
        base.BaseInteract();
        _controlLoading.RemoveScene(listScene);
    }
}

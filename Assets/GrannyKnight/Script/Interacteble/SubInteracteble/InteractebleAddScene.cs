using UnityEngine;
using Zenject;

public class InteractebleAddScene : Interacteble
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
        _controlLoading.AddScene(listScene);
    }
}

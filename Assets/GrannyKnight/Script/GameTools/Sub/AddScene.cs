using UnityEngine;
using Zenject;

public class AddScene : MonoBehaviour
{
    [SerializeField] private ListScene listScene;
    private GameManager _controlLoading;

    [Inject]
    private void Construct(GameManager controlLoading)
    {
        _controlLoading = controlLoading;
    }

    public void Active()
    {
        _controlLoading.AddScene(listScene);
    }
}

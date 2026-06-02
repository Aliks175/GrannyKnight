using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class RemoveScene : MonoBehaviour
{
    [SerializeField] private ListScene listScene;
    private GameManager _controlLoading;

    [Inject]
    public void Construct(GameManager controlLoading)
    {
        _controlLoading = controlLoading;
    }

    public void Active()
    {
        _controlLoading.RemoveScene(listScene);
    }
}
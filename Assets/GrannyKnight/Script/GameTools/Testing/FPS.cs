using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int _Value;

    private void Start()
    {
        Application.targetFrameRate = _Value;
    }
}
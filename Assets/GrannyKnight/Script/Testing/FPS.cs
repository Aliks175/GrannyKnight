using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int _Value;

    private void OnValidate()
    {
        Application.targetFrameRate = _Value;
    }
}
using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private bool _IsRotateOnlyForY;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_IsRotateOnlyForY)
        {
            transform.rotation = Quaternion.Euler(0f, _camera.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = _camera.transform.rotation;
        }
    }
}
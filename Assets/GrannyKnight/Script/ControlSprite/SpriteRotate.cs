using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private bool _IsRotateOnlyForY;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (_IsRotateOnlyForY)
        {
            transform.rotation = Quaternion.Euler(0f, _cameraTransform.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = _cameraTransform.rotation;
        }
    }
}
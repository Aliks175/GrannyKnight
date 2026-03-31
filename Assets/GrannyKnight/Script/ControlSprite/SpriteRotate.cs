using UnityEngine;
using Zenject;

public class SpriteRotate : MonoBehaviour
{
    [SerializeField] private bool _IsRotateOnlyForY;
    private Transform _cameraTransform;
    private bool _isReady => _cameraTransform != null;

    [Inject]
    public void Construct(Camera camera)
    {
        _cameraTransform = camera.transform;
    }

    private void Update()
    {
        if (!_isReady) { return; }
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
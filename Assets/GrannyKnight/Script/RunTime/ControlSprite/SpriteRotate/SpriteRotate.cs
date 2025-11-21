using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private bool _IsRotateOnlyForY;
    [SerializeField] private float _speedLerp;

    private float eulerAnglesY;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        //eulerAnglesY = _cameraTransform.rotation.eulerAngles.y;
    }

    //private void LateUpdate()
    //{
    //    if (_IsRotateOnlyForY)
    //    {
    //        //transform.rotation = Quaternion.Euler(0f, _cameraTransform.rotation.eulerAngles.y, 0f);
    //    }
    //    else
    //    {
    //        transform.rotation = _cameraTransform.rotation;
    //    }
    //}

    private void Update()
    {
        if (_IsRotateOnlyForY)
        {
            //eulerAnglesY = Mathf.Lerp(eulerAnglesY, _cameraTransform.rotation.eulerAngles.y, _speedLerp * Time.deltaTime) ;
            //transform.rotation = Quaternion.Euler(0f, eulerAnglesY, 0f);
            transform.rotation = Quaternion.Euler(0f, _cameraTransform.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = _cameraTransform.rotation;
        }
    }
}
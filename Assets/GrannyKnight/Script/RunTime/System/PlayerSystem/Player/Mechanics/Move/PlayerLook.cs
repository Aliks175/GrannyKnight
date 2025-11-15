using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private GameObject _headSlot;
    [SerializeField] private float _ySensitivity = 30f;
    [SerializeField] private float _xSensitivity = 30f;
    private float _xRotation = 0;
    private bool _isPlay;

    public void Initialization()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _isPlay = true;
    }

    public void ProcessLook(Vector2 vector2)
    {
        if (!_isPlay) return;
        float mouseX = vector2.x;
        float mouseY = vector2.y;
        _xRotation -= (mouseY * Time.deltaTime) * _ySensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -70, 70);
        _headSlot.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate((mouseX * Time.deltaTime) * _xSensitivity * Vector3.up);
    }

    public void ControlPlay(bool play)
    {
        _isPlay = play;
    }
}
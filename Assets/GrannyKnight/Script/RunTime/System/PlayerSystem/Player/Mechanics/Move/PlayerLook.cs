using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private GameObject _headSlot;
    [SerializeField] private float _ySensitivity = 30f;
    [SerializeField] private float _xSensitivity = 30f;
    [Range(0,1)][SerializeField] private float _koefAim = 0.5f;
    private bool _isAim = false;
    private float _xRotation = 0;
    private bool _isPlay;

    public void Initialization()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _isPlay = true;
    }

    public void UnLockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void ProcessLook(Vector2 vector2)
    {
        if (!_isPlay) return;
        float mouseX = vector2.x;
        float mouseY = vector2.y;
        if (_isAim)
        {
            mouseX *= _koefAim;
            mouseY *= _koefAim;
        }
        _xRotation -= (mouseY * Time.deltaTime) * _ySensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -70, 70);
        _headSlot.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        
        transform.Rotate((mouseX * Time.deltaTime) * _xSensitivity * Vector3.up);
    }

    public void ControlPlay(bool play)
    {
        _isPlay = play;
    }
    public bool IsAim
    {
        set => _isAim = value;
    }
}
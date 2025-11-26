using UnityEngine;
using UnityEngine.Events;

public class SpriteRotateDirectional : MonoBehaviour
{
    [Header("Parametrs")]
    [SerializeField, Range(0f, 180f)] private float _backAngle = 65f;
    [SerializeField, Range(0f, 180f)] private float _sideAngle = 155f;
    [Header("Sub Object")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _transform;
    [SerializeField] private Animator _animator;
    [SerializeField] private Camera _camera;
    

    private Vector2 _backSide = new Vector2(0f, -1f);
    private Vector2 _frontSide = new Vector2(0f, 1f);
    private Vector2 _leftSide = new Vector2(1f, 0f);
    private Vector2 _rightSide = new Vector2(-1f, 0f);

    private int _moveX;
    private int _moveY;

    private void Start()
    {
        _moveX = Animator.StringToHash("MoveX");
        _moveY = Animator.StringToHash("MoveY");
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 cameraForward = _camera.transform.forward;
        cameraForward = new Vector3(cameraForward.x, 0f, cameraForward.z);

        float singleAngle = Vector3.SignedAngle(_transform.forward, cameraForward, Vector3.up);
        Vector2 animationDirection = _backSide;
        float angle = Mathf.Abs(singleAngle);

        if (angle < _backAngle)
        {
            animationDirection = _backSide;
        }
        else if (angle < _sideAngle)
        {
            if (singleAngle < 0f)
            {
                animationDirection = _rightSide;
            }
            else
            {
                animationDirection = _leftSide;
            }
        }
        else
        {
            animationDirection = _frontSide;
        }
        _animator.SetFloat(_moveX, animationDirection.x);
        _animator.SetFloat(_moveY, animationDirection.y);
    }
    public void PickUpSet()
    {
        _animator.SetBool("PickUp", true);
    }
    public void Dead()
    {
        _animator.SetTrigger("Dead");
    }
}
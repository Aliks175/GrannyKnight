using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SpriteRotateDirectional : MonoBehaviour
{
    [Header("Parametrs")]
    [SerializeField, Range(0f, 180f)] private float _backAngle = 65f;
    [SerializeField, Range(0f, 180f)] private float _sideAngle = 155f;
    [Header("Sub Object")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _transform;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _cameraTransform;
    private SystemBuss _systemBuss;

    private Vector2 _backSide = new Vector2(0f, -1f);
    private Vector2 _frontSide = new Vector2(0f, 1f);
    private Vector2 _leftSide = new Vector2(1f, 0f);
    private Vector2 _rightSide = new Vector2(-1f, 0f);

    private int _moveX;
    private int _moveY;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    private void OnEnable()
    {
        WaitPlayer().Forget();
    }

    private void Start()
    {
        _moveX = Animator.StringToHash("MoveX");
        _moveY = Animator.StringToHash("MoveY");
    }

    private void LateUpdate()
    {
        Vector3 cameraForward = _cameraTransform.position - transform.position;
        cameraForward.y = 0;
        //Vector3 cameraForward = _cameraTransform.transform.forward;
        //cameraForward = new Vector3(cameraForward.x, 0f, cameraForward.z);

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

    public void SetTarget(Transform player)
    {
        _cameraTransform = player;
    }

    public void PickUpSet()
    {
        _animator.SetBool("PickUp", true);
    }

    public void Dead()
    {
        _animator.SetTrigger("Dead");
    }

    private async UniTaskVoid WaitPlayer()
    {
        if (_systemBuss == null) { return; }
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        SetTarget(playerCharacter.transform);
    }
}
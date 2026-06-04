using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;


public class StaticRotate : MonoBehaviour
{
    [SerializeField] private bool _IsRotateOnlyForY;
    [SerializeField,TextArea(3,8)] private string _test;
    private Transform _cameraTransform;
    private SystemBuss _systemBuss;
    private bool _isReady => _cameraTransform != null;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    private void OnEnable()
    {
        if (_systemBuss == null) { return; }
        WaitPlayer().Forget();
    }

    public void SetTarget(Transform player)
    {
        _cameraTransform = player;
    }

    public void OnUpdate()
    {
        _test = $"_isReady = {_isReady} OnUpdate {transform.rotation}";
        if (!_isReady) { return; }
        Vector3 tempDirection = transform.position - _cameraTransform.position;
        tempDirection.y = _IsRotateOnlyForY ? 0 : tempDirection.y;
        transform.rotation = Quaternion.LookRotation(tempDirection);
    }

    private void SetPlayer(PlayerCharacter player)
    {
        SetTarget(player.transform);
    }

    private async UniTaskVoid WaitPlayer()
    {
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        SetPlayer(playerCharacter);
    }



    //private void Update()
    //{
    //    if (!_isReady) { return; }

    //    Vector3 tempDirection = transform.position - _cameraTransform.position;
    //    tempDirection.y = _IsRotateOnlyForY ? 0 : tempDirection.y;
    //    transform.rotation = Quaternion.LookRotation(tempDirection);
    //}
}

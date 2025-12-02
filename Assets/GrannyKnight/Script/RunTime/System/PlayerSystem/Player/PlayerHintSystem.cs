using UnityEngine;

public class PlayerHintSystem : MonoBehaviour
{
    [SerializeField] private Wisp _wisp;
    private Transform _activeTarget;
    private Wisp _promptWisper;
    private bool _isLockWisp;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        if (_promptWisper == null)
        {
            _promptWisper = Instantiate(_wisp, transform.position, Quaternion.identity);
            _promptWisper.Initialization();
            _promptWisper.gameObject.SetActive(false);
        }
        _isLockWisp = false;
    }

    public void ControlWisp(bool isOn)
    {
        Debug.Log($"ControlWisp = {isOn}");
        _isLockWisp = isOn;
    }

    public void SetTarger(Transform transform)
    {
        Debug.Log(transform.gameObject.name);
        _activeTarget = transform;
    }

    public void MoveTarget()
    {
        if (_isLockWisp) return;
        if (_activeTarget == null) return;
        if (_promptWisper != null)
        {
            _promptWisper.transform.position = transform.position;
            _promptWisper.gameObject.SetActive(true);
            _promptWisper.MoveToTarget(_activeTarget);
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

public class UseTrigerItem : MonoBehaviour, IItemUseble
{
    [SerializeField] private ShowPrompt _showPrompt;
    public UnityEvent PlayerTriggerEnter;
    public UnityEvent PlayerTriggerExit;
    public UnityEvent PlayerUse;
    private bool _isReadyActive;

    private void Awake()
    {
        _isReadyActive = false;
    }

    public void Interact()
    {
        if (!_isReadyActive) { return; }
        PlayerUse?.Invoke();
        _isReadyActive = false;
        if (_showPrompt == null) { return; }
        _showPrompt.ControlShow(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerCharacter player))
        {
            player = other.GetComponentInParent<PlayerCharacter>();
        }

        if (player == null) { return; }
        if (_isReadyActive) { return; }

        player.SetUseItem(this);
        //Debug.Log("Enter");
        _isReadyActive = true;
        PlayerTriggerEnter?.Invoke();
        if (_showPrompt == null) { return; }
        _showPrompt.ControlShow(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Const.PlayerTag))
        {
            if (!_isReadyActive) { return; }
            Debug.Log("Exit");
            _isReadyActive = false;
            PlayerTriggerExit?.Invoke();
            if (_showPrompt == null) { return; }
            _showPrompt.ControlShow(false);
        }
    }
}
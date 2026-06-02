using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class UseTrigerItem : MonoBehaviour, IItemUseble
{
    [SerializeField] private ShowPrompt _showPrompt;
    public UnityEvent PlayerTriggerEnter;
    public UnityEvent PlayerTriggerExit;
    public UnityEvent PlayerUse;
    private PlayerCharacter _playerCharacter;
    private SystemBuss _systemBuss;
    private bool _isReadyActive;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
        _isReadyActive = false;
    }

    private void Start()
    {
        WaitPlayer().Forget();
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
        if (other.CompareTag(Const.PlayerTag))
        {
            if (_playerCharacter == null)
            {
                WaitPlayer().Forget();
            }
            else if (!_isReadyActive)
            {
                SetItem();
            }
        }
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

    private async UniTaskVoid WaitPlayer()
    {
        PlayerCharacter playerCharacter = await _systemBuss.GetPlayer();
        _playerCharacter = playerCharacter;
        if (!_isReadyActive)
        {
            SetItem();
        }
    }

    private void SetItem()
    {
        if (_playerCharacter == null) { return; }
        _playerCharacter.SetUseItem(this);
        _isReadyActive = true;
        PlayerTriggerEnter?.Invoke();
        if (_showPrompt == null) { return; }
        _showPrompt.ControlShow(true);
    }
}
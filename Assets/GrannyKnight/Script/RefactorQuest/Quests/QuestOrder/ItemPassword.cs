using UnityEngine;
using Zenject;

public class ItemPassword : MonoBehaviour
{
    public int Number => _number;
    [SerializeField] private GameObject _activeItem;
    [SerializeField] private int _number;
    private PasswordButton _passwordButton;
    private QuestPasswordSelection _quest;

    [Inject]
    public void Construct(QuestPasswordSelection quest)
    {
        _quest = quest;
        _quest.RegisterButton(this);
    }

    private void OnEnable()
    {
        if (_passwordButton == null)
        {
            _passwordButton = GetComponentInChildren<PasswordButton>(true);
            //Debug.Log($"PasswordButton = Null ? {_passwordButton == null}");
            _passwordButton.OnInteract += OnInteract;
        }
    }

    private void OnDisable()
    {
        _passwordButton.OnInteract -= OnInteract;
    }

    public void StateActiveItem()
    {
        ControlActiveItem(true);
    }

    public void ResetItem()
    {
        ControlActiveItem(false);
    }

    private void OnInteract()
    {
        _quest.CheckItem(this);
    }

    private void ControlActiveItem(bool isActive)
    {
        _passwordButton.gameObject.SetActive(!isActive);
        if (_activeItem != null)
        {
            _activeItem.SetActive(isActive);
        }
    }
}
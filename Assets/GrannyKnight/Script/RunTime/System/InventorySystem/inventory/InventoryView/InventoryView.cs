using UnityEngine;
using UnityEngine.Events;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    public UnityEvent Show;
    public UnityEvent Hide;

    public void Initialization()
    {
        if (_inventoryPanel == null)
        {
            Debug.LogError($"InventoryView - Not found -> InventoryPanel");
        }
    }

    public void ControlShowInventory()
    {
        if (_inventoryPanel.activeSelf)
        {
            Hide?.Invoke();
            Cursor.lockState = CursorLockMode.Locked;
            _inventoryPanel.SetActive(false);
        }
        else
        {
            Show?.Invoke();
            Cursor.lockState = CursorLockMode.Confined;
            _inventoryPanel.SetActive(true);
        }
    }
}
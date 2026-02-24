using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using VHierarchy.Libs;
public class DragManager : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject draggedObject;
    private Vector3 offset;
    private float dragDepth;
    private Vector3 lockedPosition;
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;
    [SerializeField] private Vector3 _ofsetForRb;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (isDragging && draggedObject != null)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            if (lockX) newPosition.x = lockedPosition.x + _ofsetForRb.x;
            if (lockY) newPosition.y = lockedPosition.y + _ofsetForRb.y;
            if (lockZ) newPosition.z = lockedPosition.z + _ofsetForRb.z;
            draggedObject.transform.position = newPosition;
        }
    }

    public void OnMouseButton(InputAction.CallbackContext callback)
    {
        switch (callback.phase)
        {
            case InputActionPhase.Started:
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.CompareTag("Draggable"))
                    {
                        draggedObject = hit.collider.gameObject;
                        lockedPosition = draggedObject.transform.position;
                        dragDepth = Vector3.Dot(hit.point - mainCamera.transform.position, mainCamera.transform.forward);
                        offset = draggedObject.transform.position - hit.point;
                        if (draggedObject.GetComponent<Rigidbody>() != null)
                        {
                            draggedObject.GetComponent<Rigidbody>().isKinematic = true;
                        }
                        isDragging = true;
                    }
                }
                break;
            case InputActionPhase.Canceled:
                isDragging = false;
                if (draggedObject != null && draggedObject?.GetComponent<Rigidbody>() != null)
                {
                    draggedObject.GetComponent<Rigidbody>().isKinematic = false;
                    draggedObject = null;
                }
                break;
        }
    }
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 planePoint = mainCamera.transform.position + mainCamera.transform.forward * dragDepth;
        Plane plane = new Plane(-mainCamera.transform.forward, planePoint);
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return draggedObject.transform.position;
    }
}
using UnityEngine;

public class StartQuest : MonoBehaviour
{
    [SerializeField] private Quest _creater;
    [SerializeField] private LayerMask _playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1, _playerLayer);
        if (colliders.Length > 0 && colliders != null)
        {
            _creater.StartQuest();
        }
    }
}
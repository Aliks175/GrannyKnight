using UnityEngine;

public class PlayerHintSystem : MonoBehaviour
{
    [SerializeField] private GameObject _wisp;
    private Transform _transform;
    public Transform TransformQuest
    {
        set
        {
            _transform = value;
        }
    }
    public void SetTarget()
    {
        GameObject temp = Instantiate(_wisp, _transform.position, Quaternion.identity);
        temp.GetComponent<Wisp>().MoveToTarget(_transform);
    }
}

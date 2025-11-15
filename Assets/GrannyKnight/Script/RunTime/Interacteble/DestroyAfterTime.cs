using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;
    void Awake()
    {
        Destroy(gameObject, _timeToDestroy);
    }

}

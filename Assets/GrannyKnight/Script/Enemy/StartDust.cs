using UnityEngine;

public class StartDust : MonoBehaviour
{
    [SerializeField] private DustCreater _creater;
    void OnTriggerEnter(Collider other)
    {
        _creater.CreateOnStart();
    }
}

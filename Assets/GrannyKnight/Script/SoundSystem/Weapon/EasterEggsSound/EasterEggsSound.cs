using FMODUnity;
using UnityEngine;

public class EasterEggsSound : MonoBehaviour
{
    [SerializeField] private EventReference _fire;

    public void Fire()
    {
        if (!_fire.IsNull)
        {
            RuntimeManager.PlayOneShot(_fire);
        }
    }
}
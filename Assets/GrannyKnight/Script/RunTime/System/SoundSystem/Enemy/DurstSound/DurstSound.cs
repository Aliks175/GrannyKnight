using FMODUnity;
using UnityEngine;

public class DurstSound : MonoBehaviour
{
    [SerializeField] private EventReference _hit;
    [SerializeField] private EventReference _die;


    public void OnHit()
    {
        if (!_hit.IsNull)
        {
            RuntimeManager.PlayOneShot(_hit);
        }
    }

    public void OnDie()
    {
        if (!_die.IsNull)
        {
            RuntimeManager.PlayOneShot(_die);
        }
    }
}
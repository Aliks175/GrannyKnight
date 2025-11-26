using UnityEngine;
using Unity.Cinemachine;
using DG.Tweening;

public class ChangeFOV : MonoBehaviour
{
    [SerializeField] private float _targetFOV;
    [SerializeField] private float _duration;
    [SerializeField] private PlayerAim _playerAim;
    public void Change()
    {
        _playerAim.StartAim(_targetFOV,_duration);
    }
    public void ResetFOV()
    {
        _playerAim.StopAim();
    }
}

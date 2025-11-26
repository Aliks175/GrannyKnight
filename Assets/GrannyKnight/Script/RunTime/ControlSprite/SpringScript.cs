using DG.Tweening;
using UnityEngine;

public class SpringScript : MonoBehaviour
{
    [SerializeField] private float _springTime;
    [Tooltip("На сколько процентов увеличится объект(если поставить 0.1, то увеличится на 10%)")][SerializeField] private Vector3 _springForce;
    [SerializeField] private int _springCount = 2;
    [Range(0,1)][SerializeField] private float _sprintCoef;
    [SerializeField] private bool _isOnEnable;
    private Tween _tween;
    private void OnEnable()
    {
        if (_isOnEnable)
        {
            StartSpring();
        }
    }
    private void OnDisable()
    {
        if (_isOnEnable)
        {
            StopSpring();
        }
    }
    public void StartSpring()
    {
        _tween = transform.DOPunchScale(_springForce,_springTime,_springCount,_sprintCoef).SetLoops(-1, LoopType.Restart).SetEase(Ease.OutBounce);
        _tween.Play();
    }
    public void StopSpring()
    {
        _tween.Kill();
    }
}

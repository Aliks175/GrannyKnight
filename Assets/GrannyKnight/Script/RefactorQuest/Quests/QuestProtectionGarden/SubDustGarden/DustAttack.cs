using DG.Tweening;
using UnityEngine;

public class DustAttack
{
    private Transform _body;
    private DangerZone _dangerZone;
    private float _timeWaitTouch;
    private float _timeNextTouch;
    private int _countTouch;
    private int _maxTouch;
    private bool _isAttack;

    public void Start(DangerZone dangerZone, Transform body, int maxTouch, float timeWaitTouch)
    {
        _dangerZone = dangerZone;
        _body = body;
        _maxTouch = maxTouch;
        _timeWaitTouch = timeWaitTouch;
        _dangerZone.OnEnter += Damage;
    }

    public void Dispose()
    {
        _dangerZone.OnEnter -= Damage;
    }

    private void Damage(Collider collider)
    {
        if (Time.time > _timeNextTouch)
        {
            _timeNextTouch = Time.time + _timeWaitTouch;
            _countTouch++;
            if (_countTouch > 3)
            {
                Attack(collider);
            }
            else
            {
                _body.DOScale(_countTouch, 1f).Play();
            }
        }
       
    }

    private void Attack(Collider collider)
    {
        if (_isAttack) { return; }
        if (_dangerZone.CheckPlayer(_body.position, 2))
        {

        }
        
    }
}
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class TargetDust : MonoBehaviour , IHealtheble
{
    private float _health;
    [SerializeField] private SpriteRenderer _sprite;
    private float _speed;
    private DustCreater _creater;
    private int _stage;
    private Transform _endPoint;
    private Tween _tween;

    private void OnDisable()
    {
        _tween.Kill();
    }

    public void SetParameters(StageDust stage, DustCreater creater, Transform distance, int index)
    {
        _sprite.color = stage.ColorStage;
        _speed = stage.SpeedStage;
        _health = stage.HealthStage;
        _creater = creater;
        _stage = index;
        _endPoint = distance;
        gameObject.transform.localScale = stage.BaseScaleStage * Vector3.one;
        StartMove();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _creater.OnDustDie(this.transform, _stage);
        Destroy(gameObject);
    }

    private void StartMove()
    {
        _tween =  transform.DOMove(_endPoint.position, _speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() => OnEndPoint());
        _tween.Play();
    }

    private void OnEndPoint()
    {
        Destroy(gameObject);
    }
}
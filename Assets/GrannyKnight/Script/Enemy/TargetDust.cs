using UnityEngine;
using DG.Tweening;

public class TargetDust : MonoBehaviour , IHealtheble
{
    private float _health;
    private Color _colorStart;
    private float _speed;
    private DustCreater _creater;
    private int _stage;
    private Transform _endPoint;
    private float _scale, _scaleStep;
    public void SetParameters(StageDust stage, DustCreater creater, Transform distance)
    {
        _colorStart = stage.ColorStage;
        _speed = stage.SpeedStage;
        _health = stage.HealthStage;
        _creater = creater;
        _stage = stage.Stage;
        _endPoint = distance;
        gameObject.transform.localScale = stage.BaseScaleStage * Vector3.one;
        _scale = stage.BaseScaleStage;
        _scaleStep = (stage.MaxScaleStage - stage.BaseScaleStage) / _health;
        GetComponent<Renderer>().material.color = _colorStart;
        StartMove();
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        _scale += _scaleStep;
        gameObject.transform.DOScale(_scale, 0.1f);
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
        transform.DOMove(_endPoint.position, _speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() => OnEndPoint());
    }
    private void OnEndPoint()
    {
        Destroy(gameObject);
    }
}

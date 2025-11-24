using DG.Tweening;
using UnityEngine;

public class TargetDust : MonoBehaviour, IHealtheble
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _distanceForPlayer;
    private DustCreater _creater;
    private Transform _endPoint;
    private Tween _tween;
    private float _health;
    private float _speed;
    private int _stage;
    private bool _isPlay;

    private void OnDisable()
    {
        _tween?.Kill();
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
        _isPlay = true;
        StartMove();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _creater.Damage(damage);
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
        // Движение к динамической позиции через Update
    }

    private void Update()
    {
        if (!_isPlay) return;
        if (_endPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint.position, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _endPoint.position) < _distanceForPlayer)
            {
                OnEndPoint();
            }
        }
    }

    private void OnEndPoint()
    {
        if (!_isPlay) return;
        _isPlay = false;
        _endPoint = null;

        _creater.StopQuest(QuestEnding.Bad);
        Destroy(gameObject);
    }
}
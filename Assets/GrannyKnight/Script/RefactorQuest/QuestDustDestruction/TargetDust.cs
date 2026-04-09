using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class TargetDust : MonoBehaviour, IHealtheble
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _timeToTakeDamage;
    private QuestDustDestruction _creater;
    private Transform _endPoint;
    private Tween _tweenTakeDamage;
    private float _health;
    private float _speed;
    private int _stage;
    private bool _isPlay;
    private Color _alpha;

    public UnityEvent OnDie;
    public UnityEvent OnHit;

    private void OnDisable()
    {
        _tweenTakeDamage?.Kill();
    }

    public void SetParameters(StageDust stage, QuestDustDestruction creater, Transform distance, int index)
    {
        _sprite.color = stage.ColorStage;
        _alpha = new Color(stage.ColorStage.r, stage.ColorStage.g, stage.ColorStage.b, 0f);
        _speed = stage.SpeedStage;
        _health = stage.HealthStage;
        _creater = creater;
        _stage = index;
        _endPoint = distance;
        gameObject.transform.localScale = stage.BaseScaleStage * Vector3.one;
        _isPlay = true;
        SetSpriteRotate();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _tweenTakeDamage?.Kill();
        _creater.Damage(damage);
        _sprite.color = _alpha;
        _tweenTakeDamage = _sprite.DOFade(1f, _timeToTakeDamage);
        _tweenTakeDamage.Play();
        if (_health <= 0)
        {
            OnDie?.Invoke();
            Die();
            return;
        }
        OnHit?.Invoke();
    }

    private void Die()
    {
        _creater.OnDustDie(this, _stage);
        gameObject.SetActive(false);
    }

    private void ChangePlay()
    {
        _isPlay = true;
    }

    private void SetSpriteRotate()
    {
        SpriteRotate spriteRotate = GetComponentInChildren<SpriteRotate>();
        if (spriteRotate != null )
        {
            spriteRotate.SetTarget(_endPoint);
        }
    }

    private void Update()
    {
        if (!_isPlay) return;
        if (_endPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint.position, _speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHealthSystem player))
        {
            if (!_isPlay) return;
            int health = player.TakeDamage();
            //Debug.Log("Damage on dust: " + health);
            _creater.CheckHealth(health);
            _isPlay = false;
            transform.position = this.transform.position - transform.forward;
            Invoke("ChangePlay", 2f);
        }
    }
}
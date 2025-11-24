using DG.Tweening;
using UnityEngine;

public class ArrowWeapon : MonoBehaviour
{
    private Tween _tween;
    private float _damage;

    public void StartFlight(float force, float angle, Vector3[] path, float damage)
    {
        _damage = damage;

        // Рассчитываем параметры полета
        float flightDistance = CalculateFlightDistance(force, angle);
        float flightTime = CalculateFlightTime(force, flightDistance);


        // Запускаем анимацию
        _tween = transform.DOPath(path, flightTime, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                //.OnUpdate(OnProjectileUpdate)
                .OnComplete(OnProjectileLanded);
        _tween.Play();

        Debug.Log($"Выстрел с силой: {force:F1}, дистанция: {flightDistance:F1}");

        // Сбрасываем силу после выстрела
        force = 0f;
    }

    private float CalculateFlightTime(float force, float distance)
    {
        // Чем больше сила, тем быстрее полет (меньше времени)
        float speed = force * 2f; // Скорость пропорциональна силе
        return distance / speed;
    }

    private float CalculateFlightDistance(float force, float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        float distance = (force * force * Mathf.Sin(2 * angleRad)) / Physics.gravity.magnitude;
        return Mathf.Abs(distance) * 10f; // Увеличиваем дальность в 10 раз
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHealtheble target))
        {
            target.TakeDamage(_damage);
            Debug.Log(other.gameObject.name);
            Destroy(this.gameObject);
        }
    }

    private void OnProjectileLanded()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject,3f);
    }
}


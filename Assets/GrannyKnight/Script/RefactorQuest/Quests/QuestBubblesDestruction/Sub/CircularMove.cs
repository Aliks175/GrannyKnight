using Cysharp.Threading.Tasks;
using UnityEngine;

public class CircularMove : MonoBehaviour
{
    [Header("CircularMove")]
    [SerializeField] private float _radius;
    [SerializeField] private float _frequency;
    [SerializeField] private float _coefficientYRadius;
    [SerializeField] private float _coefficientYFrequency;

    private Vector3 _startPosition;
    private float _time;
    private bool _isPlay;

    private void Start()
    {
        _startPosition = transform.position;
        _isPlay = false;
        RundomRange().Forget();
    }

    private void Update()
    {
        if (_isPlay)
        {
            CircularLoopMove();
        }
    }

    private async UniTask RundomRange()
    {
        float _timeWait = Random.Range(0, 1f);
        await UniTask.Delay(System.TimeSpan.FromSeconds(_timeWait));
        _isPlay = true;
    }

    private void CircularLoopMove()
    {
        _time += Time.deltaTime;
        float x = Mathf.Cos(_time * _frequency) * _radius;
        float y = Mathf.Cos(_time * _coefficientYFrequency) * _coefficientYRadius;
        float z = Mathf.Sin(_time * _frequency) * _radius;
        Vector3 temp = new(_startPosition.x + x, _startPosition.y + y, _startPosition.z + z);
        transform.position = temp;
    }
}
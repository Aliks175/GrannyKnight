using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    [SerializeField] private Transform _hitMarker;
    [SerializeField] private LayerMask _layerIgnor;
    [SerializeField] private int _maxPoints = 30;
    private LineRenderer _lineRenderer;
    private Vector3[] _points;

    private const float _wimeWait = 0.1f;
    private const float _coefficient = 2f;
    private const float offset = 0.035f;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        ControlVisible(false);
    }

    public void ControlVisible(bool visible)
    {
        _lineRenderer.enabled = visible;
        _hitMarker.gameObject.SetActive(visible);
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3 originPosition = origin;
        _points = new Vector3[_maxPoints];
        for (int i = 0; i < _points.Length; i++)
        {
            float time = i * _wimeWait;
            Vector3 nextPoint = origin + speed * time + Physics.gravity * time * time / _coefficient;
            Vector3 direction = nextPoint - originPosition;

            _points[i] = nextPoint;
            if (Physics.Raycast(originPosition, direction.normalized, out RaycastHit hit, direction.magnitude, _layerIgnor))
            {
                MoveHitMarker(hit);
                _lineRenderer.positionCount = i;
                break;
            }
            else
            {
                _hitMarker.gameObject.SetActive(false);
            }
            originPosition = nextPoint;
        }
        _lineRenderer.SetPositions(_points);
    }

    private void MoveHitMarker(RaycastHit hit)
    {
        _hitMarker.gameObject.SetActive(true);
        _hitMarker.position = hit.point + hit.normal * offset;
        _hitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
    }
}
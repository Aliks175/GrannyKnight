using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private const float _wimeWait = 0.1f;
    private const float _coefficient = 2f;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void ControlVisible(bool visible)
    {
        _lineRenderer.enabled = visible;
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[30];
        _lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * _wimeWait;
            points[i] = origin + speed * time + Physics.gravity * time * time / _coefficient;
        }
        _lineRenderer.SetPositions(points);
    }

}

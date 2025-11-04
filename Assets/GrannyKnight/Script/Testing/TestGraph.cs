using UnityEngine;

public class TestGraph : MonoBehaviour
{
    public AnimationCurve Curve;

    private void Update()
    {
        Keyframe keyframe = new Keyframe(Time.time, transform.eulerAngles.y, 0, 0, 0, 0);
        Curve.AddKey(keyframe);
    }
}
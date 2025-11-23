using DG.Tweening;
using UnityEngine;

public class ScrollingDrum : MonoBehaviour
{
    [SerializeField] private float _speedScroll;
    [SerializeField] private Transform Drum;
    Vector3 _endRotation;
    private Tween _rotation;

    private void Start()
    {
        _endRotation = new Vector3(360f,0,0);
    }

    public void StartRotation()
    {
        _rotation = Drum.DOLocalRotate(_endRotation, _speedScroll, RotateMode.FastBeyond360).From(Vector3.zero).SetLoops(-1,LoopType.Restart).OnComplete(()=> Drum.transform.localRotation = Quaternion.identity);
    }

    public void StopRotation()
    {
        _rotation?.Kill(true);
    }
}

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
        _endRotation = new Vector3(355f,0,0);
    }

    public void StartRotation()
    {
        Debug.Log("TryRotation");
        if(_rotation != null) return;
        Debug.Log($"Drum = {Drum.localPosition}");
        //_rotation = Drum.DOLocalRotate(_endRotation, _speedScroll, RotateMode.FastBeyond360).From(Vector3.zero).SetLoops(-1,LoopType.Restart).OnComplete(()=> Drum.transform.localRotation = Quaternion.identity);
        _rotation = Drum.DOLocalRotate(_endRotation, _speedScroll, RotateMode.FastBeyond360);//.From(Vector3.zero)//.SetLoops(-1, LoopType.Restart);
        _rotation.Play();
    }

    public void StopRotation()
    {
        _rotation?.Kill(true);
    }
}

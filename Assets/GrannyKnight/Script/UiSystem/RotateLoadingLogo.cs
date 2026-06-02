using DG.Tweening;
using UnityEngine;

public class RotateLoadingLogo : MonoBehaviour
{
    private Tween tween;

    private void OnEnable()
    {
        if (tween.IsActive())
        {
            tween.Restart();
        }
    }

    private void OnDisable()
    {
        if (tween.IsActive())
        {
            tween.Pause();
        }
    }

    private void OnDestroy()
    {
        if (tween.IsActive())
        {
            tween.Kill();
        }
    }

    private void Start()
    {
        tween = transform.DOLocalRotate(new Vector3(0, 0, 360), 3, RotateMode.FastBeyond360)
             .From(Vector3.zero)
             .SetLoops(-1)
             .SetUpdate(true)
             .SetAutoKill(false);

        tween.Restart();
    }
}
using System.Collections.Generic;
using UnityEngine;

public class ViewPlayer : MonoBehaviour
{
    [SerializeField] private List<SkinnedMeshRenderer> _listHand;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void ControlViewUiPlayer(bool isVisible)
    {
        _canvasGroup.alpha = isVisible ? 1 : 0;
    }

    public void ShowPlayer()
    {
        ControlView(true);
        _canvasGroup.alpha = 1.0f;
    }

    public void HidePlayer()
    {
        ControlView(false);
        _canvasGroup.alpha = 0f;
    }

    private void ControlView(bool isVisible)
    {
        foreach (var item in _listHand)
        {
            item.enabled = isVisible;
        }
    }
}
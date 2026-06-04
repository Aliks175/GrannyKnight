using UnityEngine;
using UnityEngine.InputSystem;

public class CutsceneSkip : MonoBehaviour
{
    [SerializeField] private CanvasGroup _skiperCutscene;
    [SerializeField] private CanvasGroup _promptQuest;

    private void Awake()
    {
        _promptQuest.alpha = 0f;
        _skiperCutscene.alpha = 1f;
        CutsceneManager.OnStartCutscene += OnPanel;
        CutsceneManager.OnEndCutscene += OffPanel;
    }

    private void OnDisable()
    {
        CutsceneManager.OnStartCutscene -= OnPanel;
        CutsceneManager.OnEndCutscene -= OffPanel;
    }

    private void OnPanel()
    {
        //Debug.Log("OnPanel");
        ControlVisible(true);
    }

    private void OffPanel()
    {
        //Debug.Log("OffPanel");
        ControlVisible(false);
    }

    private void ControlVisible(bool isVisible)
    {
        _skiperCutscene.alpha = isVisible ? 1 : 0;
        _promptQuest.alpha = !isVisible ? 1 : 0;
    }

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            CutsceneManager.Instance.EndCutscene();
        }
    }
}
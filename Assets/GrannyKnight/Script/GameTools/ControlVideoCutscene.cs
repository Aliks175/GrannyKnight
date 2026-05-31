using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class ControlVidepCutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    private CutsceneScreen _cutsceneScreen;

    [Inject]
    private void Construct(CutsceneScreen cutsceneScreen)
    {
        _cutsceneScreen = cutsceneScreen;
    }

    public void PreRenderVideo()
    {
        _cutsceneScreen.PreRenderVideo(_videoPlayer);
    }

    public void StartVideo()
    {
        _cutsceneScreen.StartVideo();
    }
}
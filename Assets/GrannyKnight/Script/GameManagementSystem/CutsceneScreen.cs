using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Zenject;

public class CutsceneScreen : MonoBehaviour
{
    [SerializeField] private RawImage _renderTexture;
    [SerializeField] private Image _blackOut;
    private VideoPlayer _videoPlayer;
    private GameManager _gameManager;
    private WaitUntil _waitUntil;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _blackOut.gameObject.SetActive(false);
        _renderTexture.gameObject.SetActive(false);
        
    }

    private void LoopPoint(VideoPlayer source)
    {
        Debug.Log($"EndVideo");
        _renderTexture.gameObject.SetActive(false);
        source.loopPointReached -= LoopPoint;
    }

    public void PreRenderVideo(VideoPlayer videoPlayer)
    {
        _videoPlayer = videoPlayer;
        _videoPlayer.Prepare();
        _videoPlayer.loopPointReached += LoopPoint;
    }

    public void StartVideo()
    {
        StartCoroutine(WaitVideo());
    }

    public void OnBlackOut(bool isActive)
    {
        _blackOut.gameObject.SetActive(isActive);
    }

    private IEnumerator WaitLoad()
    {
        yield return CheckEndPreLoadScene();
        _blackOut.gameObject.SetActive(false);
    }

    private IEnumerator WaitVideo()
    {
        _blackOut.gameObject.SetActive(true);
        yield return null;
        _renderTexture.gameObject.SetActive(true);
        yield return null;
        StartCoroutine(WaitLoad());
        _videoPlayer.Play();
    }

    private WaitUntil CheckEndPreLoadScene()
    {
        if (_waitUntil == null)
        {
            _waitUntil = new WaitUntil(() => _gameManager.CurrentOperation.progress >= 0.9f);
        }
        return _waitUntil;
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Zenject;

public class TestEventEndLoadScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    //[SerializeField] private FMODEventPlayable _MODEventPlayable;
    //[SerializeField] private StudioEventEmitter _studioEvent;
    [SerializeField] private RawImage _renderTexture;
    [SerializeField] private Image _blackOut;
    private GameManager _gameManager;
    private WaitUntil _waitUntil;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _blackOut.gameObject.SetActive(false);
        _renderTexture.enabled = false;
        SceneManager.sceneLoaded += SceneLoaded;
        _videoPlayer.loopPointReached += LoopPoint;
        _videoPlayer.Prepare();
    }

    private void LoopPoint(VideoPlayer source)
    {
        Debug.Log($"EndVideo");
        _renderTexture.enabled = false;
    }

    public void StartVideo()
    {
        StartCoroutine(WaitVideo());
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log($"Scene {arg0.name} Load");
    }

    private IEnumerator WaitVideo()
    {
        _blackOut.gameObject.SetActive(true);
        yield return null;
        _renderTexture.enabled = true;
        yield return null;
        StartCoroutine(WaitLoad());
        _videoPlayer.Play();
    }

    private IEnumerator WaitLoad()
    {
        yield return CheckEndPreLoadScene();
        _blackOut.gameObject.SetActive(false);
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
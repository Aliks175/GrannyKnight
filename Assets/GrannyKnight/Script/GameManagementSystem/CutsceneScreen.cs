using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CutsceneScreen : MonoBehaviour
{
    [SerializeField] private Image _blackOut;
    private GameManager _gameManager;
    private WaitUntil _waitUntil;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
  

    private void Start()
    {
        _blackOut.gameObject.SetActive(false);
    }

    public void StartLoadScene(Action action)
    {
        StartCoroutine(WaitLoad(action));
    }

    public void OnBlackOut(bool isActive)
    {
        _blackOut.gameObject.SetActive(isActive);
    }

    private IEnumerator WaitLoad(Action action)
    {
        _blackOut.gameObject.SetActive(true);
        yield return CheckEndPreLoadScene();
        action?.Invoke();
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
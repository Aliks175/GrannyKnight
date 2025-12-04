using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class ShowSingleDialog : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private int[] _waitShowDialogs;
    private CancellationTokenSource _cts;
    private int _indexDialogPanel;
    private bool _isActive;

    private void OnEnable()
    {
        if (_cts != null)
        {
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
        }
    }

    private void OnDisable()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    public void StartDialog()
    {
        //Debug.Log($"StartDialogue ShowPanelDialog ||| {gameObject.name}");
        DialogueManager.Instance.StartDialogue(_dialogue);
        _isActive = true;
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        WaitEndDialog(_cts).Forget();
    }

    public void EndDialog()
    {
        DialogueManager.Instance.HideDialog();
        _isActive = false;
        _cts?.Cancel();
        //Debug.Log($"EndDialog - HideDialog ||| {gameObject.name}");
    }



    public void Playing()
    {
        DialogueManager.Instance.ShowDialog();
    }

    private async UniTaskVoid WaitEndDialog(CancellationTokenSource tokenSource)
    {
        _indexDialogPanel = 0;
        try
        {
            while (_indexDialogPanel < _waitShowDialogs.Length)
            {
                await UniTask.Delay(_waitShowDialogs[_indexDialogPanel] * 1000,
                    cancellationToken: tokenSource.Token
                );
                //Debug.Log($"_indexDialogPanel {_indexDialogPanel} ||  _waitShowDialogs.Length  {_waitShowDialogs.Length}");
                if (!_isActive) return;
                if (_indexDialogPanel + 1 >= _waitShowDialogs.Length)
                {
                    DialogueManager.Instance.HideDialog();
                    //Debug.Log($"WaitEndDialog - HideDialog ||| {gameObject.name}");
                    _indexDialogPanel = 0;
                    _cts?.Cancel();
                    return;
                }
                DialogueManager.Instance.NextLine();
                _indexDialogPanel++;
            }
        }
        catch (OperationCanceledException)
        {

        }
    }
}
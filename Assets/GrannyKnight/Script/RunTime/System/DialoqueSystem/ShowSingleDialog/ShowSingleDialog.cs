using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class ShowSingleDialog : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private int[] _waitShowDialogs;
    private CancellationTokenSource _cts;
    private int indexDialogPanel;

    private void OnEnable()
    {
        if (_cts != null)
        {
            _cts.Dispose();
            _cts = new CancellationTokenSource();
        }
    }

    private void OnDisable()
    {
        _cts?.Cancel();
        _cts.Dispose();
        _cts = null;
    }

    public void StartDialog()
    {
        DialogueManager.Instance.StartDialogue(_dialogue);
        _cts?.Cancel();
        WaitEndDialog().Forget();
    }

    public void EndDialog()
    {
        DialogueManager.Instance.HideDialog();
    }

    private async UniTaskVoid WaitEndDialog()
    {
        _cts = new CancellationTokenSource();
        indexDialogPanel = 0;
        try
        {
            await UniTask.Delay(_waitShowDialogs[indexDialogPanel] * 1000, true,
                cancellationToken: _cts.Token
            );
            DialogueManager.Instance.NextLine();
            indexDialogPanel++;
            if (indexDialogPanel + 1 >= _waitShowDialogs.Length)
            {
                DialogueManager.Instance.HideDialog();
                indexDialogPanel = 0;
                _cts?.Cancel();
            }
        }
        catch (OperationCanceledException)
        {

        }
    }
}
using Cysharp.Threading.Tasks;
using FMOD.Studio;
using System;
using System.Threading;
using UnityEngine;

public class DialogueManager : IDisposable
{
    private readonly LocalizationManager localization;
    private DialogueCanvas dialogueUI;

    private CancellationTokenSource cts;

    private bool skipRequested;
    private UniTaskCompletionSource dialogueFinishedTcs;

    public DialogueManager(LocalizationManager localization, string pathLocalization)
    {
        this.localization = localization;
        //localization.Load(pathLocalization);
    }

    public void Construct(DialogueCanvas dialogueCanvas)
    {
        dialogueUI = dialogueCanvas;
        dialogueUI.OnSkip += SkipLine;
    }

    public void Dispose()
    {
        dialogueUI.OnSkip -= SkipLine;
        cts?.Cancel();
        cts?.Dispose();
        cts = null;
    }

    public async UniTask StartDialogue(string filePath, string dialogueId)
    {
        await StopCurrentDialogue();

        var file = Resources.Load<TextAsset>(filePath);
        if (file == null)
        {
            Debug.LogError($"File not found: {filePath}");
            return;
        }

        var db = JsonUtility.FromJson<DialogueDatabase>(file.text);
        var dialogue = db.dialogues.Find(d => d.id == dialogueId);

        if (dialogue == null)
        {
            Debug.LogError($"Dialogue not found: {dialogueId}");
            return;
        }

        // Извлекаем имя диалога из пути файла
        string dialogueName = System.IO.Path.GetFileNameWithoutExtension(filePath);
        localization.LoadDialogueLocalization(dialogueName);

        cts = new CancellationTokenSource();
        dialogueFinishedTcs = new UniTaskCompletionSource();

        RunDialogue(dialogue, dialogueName, cts.Token).Forget();

        await dialogueFinishedTcs.Task;
    }

    private async UniTaskVoid RunDialogue(Dialogue dialogue, string dialogueName, CancellationToken token)
    {
        try
        {
            dialogueUI.Show();

            foreach (var line in dialogue.lines)
            {
                token.ThrowIfCancellationRequested();

                skipRequested = false;
                string speaker = string.Empty;
                string text = localization.GetText(dialogueName, line.key);
                if (text != string.Empty)
                {
                    speaker = localization.GetSpeakerName(line.speaker) + ": ";
                }
                
                var textTask = dialogueUI.ShowLine(speaker, text);

                EventInstance instance = default;
                bool hasInstance = false;

                UniTask soundTask = UniTask.CompletedTask;

                if (!string.IsNullOrEmpty(line.fmodEvent))
                {
                    instance = AudioManager.Play(line.fmodEvent);
                    hasInstance = true;
                    soundTask = WaitForSound(instance, token);
                }
                else
                {
                    instance = AudioManager.Play("event:/Dialogs/BaseSoundDialog");
                    hasInstance = true;
                    soundTask = WaitForSound(instance, token);
                }

                await UniTask.WhenAny(
                    UniTask.WhenAll(textTask, soundTask),
                    UniTask.WaitUntil(() => skipRequested, cancellationToken: token)
                );

                token.ThrowIfCancellationRequested();

                if (skipRequested)
                {
                    dialogueUI.Skip();
                    await UniTask.Yield();
                }

                if (hasInstance)
                {
                    instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    instance.release();
                }
            }
        }
        catch (OperationCanceledException) { }
        finally
        {
            dialogueUI.Hide();
            dialogueFinishedTcs?.TrySetResult();
        }
    }

    private async UniTask WaitForSound(EventInstance instance, CancellationToken token)
    {
        PLAYBACK_STATE state;

        while (true)
        {
            token.ThrowIfCancellationRequested();

            instance.getPlaybackState(out state);

            if (state == PLAYBACK_STATE.STOPPED)
                break;

            await UniTask.Yield();
        }
    }

    public void SkipLine()
    {
        if (skipRequested)
            return;

        skipRequested = true;

        dialogueFinishedTcs?.TrySetResult();
        dialogueUI.Skip();
    }

    public async UniTask StopCurrentDialogue()
    {
        if (cts == null)
            return;

        cts.Cancel();

        if (dialogueFinishedTcs != null)
        {
            await dialogueFinishedTcs.Task;
        }

        cts.Dispose();
        cts = null;
    }

}
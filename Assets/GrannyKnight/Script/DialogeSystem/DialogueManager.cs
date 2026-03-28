using Cysharp.Threading.Tasks;
using UnityEngine;
using FMOD.Studio;

public class DialogueManager
{
    private readonly LocalizationManager localization;
    private readonly DialogueCanvas dialogueUI;

    private bool skipRequested;
    private UniTaskCompletionSource skipTcs;

    private EventInstance currentInstance;
    private bool hasActiveInstance;

    public DialogueManager(LocalizationManager localization, DialogueCanvas dialogueUI)
    {
        this.localization = localization;
        this.dialogueUI = dialogueUI;
    }

    public async UniTask StartDialogue(DialogueID dialogueId)
    {
        var file = Resources.Load<TextAsset>(dialogueId.filePath);

        if (file == null)
        {
            Debug.LogError($"File not found: {dialogueId.filePath}");
            return;
        }

        var db = JsonUtility.FromJson<DialogueDatabase>(file.text);
        var dialogue = db.dialogues.Find(d => d.id == dialogueId.dialogueId);

        if (dialogue == null)
        {
            Debug.LogError($"Dialogue not found: {dialogueId.dialogueId}");
            return;
        }

        await PlayDialogue(dialogue);
    }

    private async UniTask PlayDialogue(Dialogue dialogue)
    {
        dialogueUI.Show(); 

        foreach (var line in dialogue.lines)
        {
            skipRequested = false;
            skipTcs = new UniTaskCompletionSource();

            string text = localization.GetText(line.key);
            string speaker = localization.GetText(line.speaker) + ": ";

            //  UI
            var textTask = dialogueUI.ShowLine(speaker, text);

            //  Звук
            UniTask soundTask = UniTask.CompletedTask;
            hasActiveInstance = false;

            if (!string.IsNullOrEmpty(line.fmodEvent))
            {
                currentInstance = AudioManager.Play(line.fmodEvent);
                hasActiveInstance = true;
                soundTask = WaitForSound(currentInstance);
            }

            //  Ждём либо завершение, либо skip
            await UniTask.WhenAny(
                UniTask.WhenAll(textTask, soundTask),
                skipTcs.Task
            );

            // если skip — убедимся что текст полностью показан
            if (skipRequested)
            {
                dialogueUI.Skip();
                await UniTask.Yield();
            }

            CleanupSound();
        }

        dialogueUI.Hide();
    }

    private async UniTask WaitForSound(EventInstance instance)
    {
        PLAYBACK_STATE state;

        while (true)
        {
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

        if (hasActiveInstance)
        {
            currentInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        skipTcs?.TrySetResult();

        dialogueUI.Skip();
    }

    private void CleanupSound()
    {
        if (!hasActiveInstance)
            return;

        currentInstance.release();
        hasActiveInstance = false;
    }
}
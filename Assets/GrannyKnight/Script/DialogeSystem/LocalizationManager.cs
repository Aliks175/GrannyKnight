using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalizationManager
{
    private Dictionary<string, Dictionary<string, string>> dialogueLocalizations = new Dictionary<string, Dictionary<string, string>>();
    private Dictionary<string, string> speakerLocalizations = new Dictionary<string, string>();
    private string currentLanguage = "ru";

    public void LoadSpeakerLocalization()
    {
        if (speakerLocalizations.Count > 0)
            return;

        string path = $"Localization/Speakers_{currentLanguage}";
        TextAsset file = Resources.Load<TextAsset>(path);
        
        if (file == null)
        {
            Debug.LogError($"Speaker localization file not found: {path}");
            return;
        }

        var wrapper = JsonUtility.FromJson<LocalizationWrapper>(file.text);
        speakerLocalizations = wrapper.ToDictionary();
    }

    public void LoadDialogueLocalization(string dialogueName)
    {
        if (dialogueLocalizations.ContainsKey(dialogueName))
            return;

        string path = $"Localization/{dialogueName}_{currentLanguage}";
        TextAsset file = Resources.Load<TextAsset>(path);
        
        if (file == null)
        {
            Debug.LogError($"Localization file not found: {path}");
            dialogueLocalizations[dialogueName] = new Dictionary<string, string>();
            return;
        }

        var wrapper = JsonUtility.FromJson<LocalizationWrapper>(file.text);
        dialogueLocalizations[dialogueName] = wrapper.ToDictionary();
    }

    public string GetText(string dialogueName, string key)
    {
        if (!dialogueLocalizations.ContainsKey(dialogueName))
        {
            LoadDialogueLocalization(dialogueName);
        }

        if (dialogueLocalizations[dialogueName].TryGetValue(key, out var value))
            return value;
        
        return key;
    }

    public string GetSpeakerName(string speakerKey)
    {
        if (speakerLocalizations.Count == 0)
        {
            LoadSpeakerLocalization();
        }

        if (speakerLocalizations.TryGetValue(speakerKey, out var value))
            return value;
        
        return speakerKey;
    }

    public void SetLanguage(string language)
    {
        currentLanguage = language;
        dialogueLocalizations.Clear();
        speakerLocalizations.Clear();
    }
}


[Serializable]
public class LocalizationWrapper
{
    public List<LocalizationItem> items;

    public Dictionary<string, string> ToDictionary()
    {
        var dict = new Dictionary<string, string>();

        foreach (var item in items)
        {
            dict[item.key] = item.value;
        }

        return dict;
    }
}

[Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}
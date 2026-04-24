using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalizationManager
{
    private Dictionary<string, string> localizedText;

    public void Load(string path)
    {
        TextAsset file = Resources.Load<TextAsset>(path);
        var wrapper = JsonUtility.FromJson<LocalizationWrapper>(file.text);
        localizedText = wrapper.ToDictionary();
    }

    public string GetText(string key)
    {
        return localizedText.TryGetValue(key, out var value) ? value : key;
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
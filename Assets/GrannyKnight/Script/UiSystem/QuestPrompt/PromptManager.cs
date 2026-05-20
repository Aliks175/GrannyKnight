using System;
using UnityEngine;
using Zenject;

public class PromptManager: IInitializable
{
    private QuestPrompt _questPrompt;

    private LocalizationManager _promptLocalization;
    private FilePrompt _currentPrompt;

    #region TestField
    private const string _oneChapter = "QuestPrompt/OneChapterPrompt";
    private const string _localizationRu = "Localization/RuPrompt";
    #endregion

    public PromptManager( LocalizationManager promptLocalization)
    {
        _promptLocalization = promptLocalization;
    }

    public void Initialize()
    {
        SetChapterPrompt(_oneChapter);
        _promptLocalization.Load(_localizationRu);
    }

    public void Construct(QuestPrompt questPrompt)
    {
        _questPrompt = questPrompt;
    }

    public void SetChapterPrompt(string path)
    {
        var file = Resources.Load<TextAsset>(path);
        if (file == null)
        {
            Debug.LogError($"File not found: {path}");
            return;
        }
        _currentPrompt = JsonUtility.FromJson<FilePrompt>(file.text);
        if (_currentPrompt == null)
        {
            Debug.LogError($"FilePrompt not found: {path}");
            return;
        }
       
        Debug.Log("FilePrompt sucsses");
    }

    public void GetPrompt(int id)
    {
        string textPrompt = null;
        DataPrompt tempDataPrompt = _currentPrompt.DataPrompts.Find(d => d.Id == id);

        if (tempDataPrompt == null)
        {
            Debug.LogError($"Prompt not found: {tempDataPrompt}");
        }
        else
        {
            textPrompt = _promptLocalization.GetText(tempDataPrompt.Key);
            _questPrompt.SetText(textPrompt);
        }
    }

   



    ////private void PrintText(List<DataPrompt> dataPrompts)
    ////{
    ////    foreach (DataPrompt prompt in dataPrompts)
    ////    {
    ////        Debug.Log($"Id: {prompt.Id} / Key: {prompt.Key}");
    ////    }
    ////}

    //private void CheckPrompt(string prompt)
    //{
    //    if (prompt == null)
    //    {
    //        Debug.LogError($"CheckPrompt not found");
    //    }
    //    else
    //    {
    //        Debug.Log(prompt);
    //    }
    //}

}
using System;
using System.Collections.Generic;

[Serializable]
public class FilePrompt
{
    public List<DataPrompt> DataPrompts;
}

[Serializable]
public class DataPrompt 
{
    public int Id;
    public string Key;
}
using System;
using System.Collections.Generic;

[Serializable]
public class DialogueDatabase
{
    public List<Dialogue> dialogues;
}

[Serializable]
public class Dialogue
{
    public string id;
    public List<DialogueLine> lines;
}

[Serializable]
public class DialogueLine
{
    public string key;
    public string fmodEvent;
    public string speaker;
}
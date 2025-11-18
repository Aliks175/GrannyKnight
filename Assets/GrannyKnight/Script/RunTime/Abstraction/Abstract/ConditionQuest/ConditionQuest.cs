using UnityEngine;


public abstract class ConditionQuest : ScriptableObject
{
    public abstract bool Evaluate(IPlayerDatable playerDatable);
}

using UnityEngine;

public class ControlDollyCart : MonoBehaviour
{
    [Header("QuestSettings")]
    [SerializeField] private MoveDollyCart[] _moveDollyCarts;

    public void Initialization()
    {
        foreach (var item in _moveDollyCarts)
        {
            item.Initialization();
        }
    }

    public void Play()
    {
        foreach (var item in _moveDollyCarts)
        {
            item.OnPlay();
        }
        Debug.Log("PlayONMoveDollyCart");
    }

    public void Stop()
    {
        foreach (var item in _moveDollyCarts)
        {
            item.OnPlay(false);
        }
        Debug.Log("PlayOFFMoveDollyCart");
    }

}

using TMPro;
using UnityEngine;

public class UiThree : MonoBehaviour
{
    [SerializeField] private GameObject _panelUi;
    [SerializeField] private GameObject _panelPrompt;
    [SerializeField] private TextMeshPro _textProgressQuest;
    private int _startcountItem;

    public void Initialization(int countItem)
    {
        _startcountItem = countItem;
    }

    public void StartTimerGame()
    {
        _panelUi.SetActive(true);
        _panelPrompt.SetActive(true);
    }

    public void Stop()
    {
        _panelUi.SetActive(false);
        _panelPrompt.SetActive(false);
    }

    public void OnUpdateUi(int countItem)
    {
        string textDiscription = $"{countItem} / {_startcountItem}";
        _textProgressQuest.SetText(textDiscription);
    }
}
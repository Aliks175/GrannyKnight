using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestResolution : MonoBehaviour
{
    [SerializeField] private SettingResolution[] _settingResolution;
    [SerializeField] private TMP_Dropdown _dropdownQuality;
    [SerializeField] private TMP_Dropdown _dropdownResolution;
    [SerializeField] private Toggle _toggleFullScreen;
    private bool _isFullScreen;

    private void Start()
    {
        _dropdownQuality.value = QualitySettings.GetQualityLevel();

        Resolution resolution = Screen.currentResolution;
        FindResol(resolution);

        Debug.Log($"currentResolution {Screen.currentResolution}");
        _isFullScreen = true;

        _toggleFullScreen.isOn = _isFullScreen;


        _dropdownQuality.onValueChanged.AddListener(SetQuality);
        _dropdownResolution.onValueChanged.AddListener(SetResolution);
        _toggleFullScreen.onValueChanged.AddListener(ChangeFullScreen);

    }

    private void FindResol(Resolution resolution)
    {
        bool iscomplited = false;
        for (int i = 0; i < _settingResolution.Length; i++)
        {
            if (resolution.width == _settingResolution[i].Width && resolution.height == _settingResolution[i].Height)
            {
                _dropdownResolution.value = i;
                Debug.Log($"Idnex FindResol {i}");
                Debug.Log($"FindResol Width {resolution.width} Height {resolution.height} isFullScreen {_isFullScreen}");
                iscomplited = true;
                break;
            }
        }

        if (!iscomplited)
        {
            Debug.Log($"FindResol NOT FOUND");
            _dropdownResolution.value = 0;
        }
    }

    #region Grapihcs

    public void SetQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
        Debug.Log($"GetQualityLevel {QualitySettings.GetQualityLevel()}");
    }

    #endregion

    #region Resolution

    public void SetResolution(int qualityindex)
    {
        if (qualityindex < 0 || qualityindex > _settingResolution.Length)
        {
            qualityindex = 0;
        }
        sad(_settingResolution[qualityindex]);
    }

    public void ChangeFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
        Resolution resolution = Screen.currentResolution;
        Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
        Debug.Log($"sad Width {resolution.width} Height {resolution.height} isFullScreen {_isFullScreen}");
    }

    private void sad(SettingResolution settingResolution)
    {
        Screen.SetResolution(settingResolution.Width, settingResolution.Height, _isFullScreen);
        Debug.Log($"currentResolution {Screen.currentResolution}");
        Debug.Log($"sad Width {settingResolution.Width} Height {settingResolution.Height} isFullScreen {_isFullScreen}");
    }
    #endregion

}

[Serializable]
public struct SettingResolution
{
    public int Width;
    public int Height;
}

//640X360
//854X480
//1024X720
//1366X768
//1600X900
//1920X1080
//1366X768
//2560X1440
//3840X2160
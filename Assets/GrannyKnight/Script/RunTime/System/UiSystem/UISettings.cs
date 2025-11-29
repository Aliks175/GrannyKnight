using FMOD.Studio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Slider _sliderMusic, _sliderSound, _sliderMaster, _sliderSensitivity;
    [SerializeField] private TMP_Text _musicText, _soundText, _masterText, _sensitivityText;
    [SerializeField] private string _musicBusPath, _soundBusPath, _masterBusPath;
    private Bus _musicBus, _soundBus, _masterBus;
    void Awake()
    {
        _musicBus = FMODUnity.RuntimeManager.GetBus(_musicBusPath);
        _soundBus = FMODUnity.RuntimeManager.GetBus(_soundBusPath);
        _masterBus = FMODUnity.RuntimeManager.GetBus(_masterBusPath);
        SetOnStart(_sliderSensitivity,_sensitivityText,SaveName.Sensitivity);
        SetOnStart(_sliderMusic,_musicText,SaveName.MusicSound);
        SetOnStart(_sliderSound,_soundText,SaveName.EffectSound);
        SetOnStart(_sliderMaster,_masterText,SaveName.MasterSound);
    }
    private void SetOnStart(Slider slider, TMP_Text text, SaveName saveName)
    {
        slider.value = PlayerPrefs.GetFloat(saveName.ToString(), 1f);
        if (saveName == SaveName.Sensitivity) text.text = PlayerPrefs.GetFloat(saveName.ToString(), 1f).ToString("0.0");
        else text.text = PlayerPrefs.GetFloat(saveName.ToString(), 1f).ToString("0");
    }
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(SaveName.MusicSound.ToString(), volume);
        PlayerPrefs.Save();
        _musicBus.setVolume(volume);
        _musicText.text = volume.ToString("0");
    }
    public void SetSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat(SaveName.EffectSound.ToString(), volume);
        PlayerPrefs.Save();
        _soundBus.setVolume(volume);
        _soundText.text = volume.ToString("0");
    }
    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(SaveName.MasterSound.ToString(), volume);
        PlayerPrefs.Save();
        _masterBus.setVolume(volume);
        _masterText.text = volume.ToString("0");
    }
    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat(SaveName.Sensitivity.ToString(), sensitivity);
        PlayerPrefs.Save();
        _sensitivityText.text = sensitivity.ToString("0.0");
    }
}

using FMOD.Studio;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [Header("SlidersSettings")]
    [SerializeField] private Slider _sliderMaster;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderDialogVoice;
    [SerializeField] private Slider _sliderSound;
    [SerializeField] private Slider _sliderSensitivity;
    [Header("SlidersSettings")]
    [SerializeField] private TextMeshProUGUI _masterText;
    [SerializeField] private TextMeshProUGUI _musicText;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private TextMeshProUGUI _soundText;
    [SerializeField] private TextMeshProUGUI _sensitivityText;
    [Header("BusPaths")]
    [SerializeField] private string _masterBusPath;
    [SerializeField] private string _musicBusPath;
    [SerializeField] private string _dialogBusPath;
    [SerializeField] private string _soundBusPath;
    private Bus _masterBus;
    private Bus _musicBus;
    private Bus _dialogBus;
    private Bus _soundBus;

    public event Action OnChangeSensity;

    //private void Awake()
    //{
    //    //_musicBus = FMODUnity.RuntimeManager.GetBus(_musicBusPath);
    //    //_soundBus = FMODUnity.RuntimeManager.GetBus(_soundBusPath);
    //    //_masterBus = FMODUnity.RuntimeManager.GetBus(_masterBusPath);
    //    //_dialogBus = FMODUnity.RuntimeManager.GetBus(_dialogBusPath);
    //    Initialization();

    //    //SetOnStart(_sliderSensitivity, _sensitivityText, SaveName.Sensitivity);
    //    //SetOnStart(_sliderMusic, _musicText, SaveName.MusicSound);
    //    //SetOnStart(_sliderSound, _soundText, SaveName.EffectSound);
    //    //SetOnStart(_sliderMaster, _masterText, SaveName.MasterSound);
    //}

    public void Initialization()
    {
        SetBus(ref _masterBus, _masterBusPath);
        SetBus(ref _musicBus, _musicBusPath);
        SetBus(ref _dialogBus, _dialogBusPath);
        SetBus(ref _soundBus, _soundBusPath);

        LoadVolumeLevel(_sliderMaster, _masterBus, SaveName.Master);
        LoadVolumeLevel(_sliderMusic, _musicBus, SaveName.Music);
        LoadVolumeLevel(_sliderDialogVoice, _dialogBus, SaveName.Dialog);
        LoadVolumeLevel(_sliderSound, _soundBus, SaveName.Sound);
        LoadSensitivity();

        SetUpVolume(_masterText, _sliderMaster, _masterBus);
        SetUpVolume(_musicText, _sliderMusic, _musicBus);
        SetUpVolume(_dialogText, _sliderDialogVoice, _dialogBus);
        SetUpVolume(_soundText, _sliderSound, _soundBus);
    }

    public void SetMasterVolume()
    {
        ChangeValue(_masterText, _sliderMaster, _masterBus);
        PlayerPrefs.SetFloat(SaveName.Master.ToString(), _sliderMusic.value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume()
    {
        ChangeValue(_musicText, _sliderMusic, _musicBus);
        PlayerPrefs.SetFloat(SaveName.Music.ToString(), _sliderMusic.value);
        PlayerPrefs.Save();
    }

    public void SetDialogVolume()
    {
        ChangeValue(_dialogText, _sliderDialogVoice, _dialogBus);
        PlayerPrefs.SetFloat(SaveName.Dialog.ToString(), _sliderDialogVoice.value);
        PlayerPrefs.Save();
    }

    public void SetSoundVolume()
    {
        ChangeValue(_soundText, _sliderSound, _soundBus);
        PlayerPrefs.SetFloat(SaveName.Sound.ToString(), _sliderSound.value);
        PlayerPrefs.Save();
    }

    public void SetSensitivity(float sensitivity)
    {
        //PlayerPrefs.SetFloat(SaveName.Sensitivity.ToString(), sensitivity);
        //PlayerPrefs.Save();
        //_sensitivityText.text = sensitivity.ToString("0.0");
        OnChangeSensity?.Invoke();
    }

    private void SetBus(ref Bus bus, string busPath)
    {
        if (busPath != "")
        {
            bus = FMODUnity.RuntimeManager.GetBus(busPath);
        }
        else
        {
            Debug.LogError("busPath = Null");
        }
    }

    private void LoadSensitivity()
    {
        float SpeedMouse = PlayerPrefs.GetFloat(SaveName.Sensitivity.ToString(), 1f);
        if (SpeedMouse <= 0f)
        {
            SpeedMouse = 1f;
        }
        _sliderSensitivity.value = SpeedMouse;
    }

    private void SetUpVolume(TextMeshProUGUI textValue, Slider slider, Bus bus)
    {
        bus.getVolume(out float volume); // получаем значение громкости шины от 0 до 1 
        slider.value = volume * slider.maxValue; // домножаем на максимальное значение слайдера , для коректного обозначения 
        ChangeValue(textValue, slider, bus); // обновляем значение на слайдере 
    }

    private void LoadVolumeLevel(Slider slider, Bus bus, SaveName saveName)
    {
        float volume = PlayerPrefs.GetFloat(saveName.ToString(), 0.5f);
        if (volume <= 0f)
        {
            volume = 0.5f;
        }
        bus.setVolume(volume / slider.maxValue);
    }

    private void ChangeValue(TextMeshProUGUI textValue, Slider slider, Bus bus)
    {
        if (textValue != null && slider != null) // проверяем что у нас есть ссылка на текстовое поле и слайдер 
        {
            textValue.SetText(slider.value.ToString("0.0")); // вводим значение из слайдера 
            bus.setVolume(slider.value / slider.maxValue); // вводим измененное значение в шину Именно здесь изменяется звук 
        }
    }


    //public void SetMusicVolume(float volume)
    //{
    //    PlayerPrefs.SetFloat(SaveName.MusicSound.ToString(), volume);
    //    PlayerPrefs.Save();
    //    _musicBus.setVolume(volume);
    //    _musicText.text = volume.ToString("0");
    //}

    //public void SetSoundVolume(float volume)
    //{
    //    PlayerPrefs.SetFloat(SaveName.EffectSound.ToString(), volume);
    //    PlayerPrefs.Save();
    //    _soundBus.setVolume(volume);
    //    _soundText.text = volume.ToString("0");
    //}
    //private void SetOnStart(Slider slider, TMP_Text text, SaveName saveName)
    //{
    //    slider.value = PlayerPrefs.GetFloat(saveName.ToString(), 1f);
    //    if (saveName == SaveName.Sensitivity) text.text = PlayerPrefs.GetFloat(saveName.ToString(), 1f).ToString("0.0");
    //    else text.text = PlayerPrefs.GetFloat(saveName.ToString(), 1f).ToString("0");
    //}

    //public void SetMasterVolume(float volume)
    //{
    //    PlayerPrefs.SetFloat(SaveName.MasterSound.ToString(), volume);
    //    PlayerPrefs.Save();
    //    _masterBus.setVolume(volume);
    //    _masterText.text = volume.ToString("0");
    //}
}
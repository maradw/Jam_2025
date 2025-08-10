using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [Header("Audio Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Mute Toggles")]
    public Toggle musicMuteToggle;
    public Toggle sfxMuteToggle;

    private void Start()
    {
        // Configurar valores iniciales
        SetupAudioSliders();
    }

    private void SetupAudioSliders()
    {
        // Configurar sliders con valores del AudioData
        masterSlider.value = AudioManager.Instance._audioData._master;
        musicSlider.value = AudioManager.Instance._audioData._music;
        sfxSlider.value = AudioManager.Instance._audioData._SFX;

        // Configurar toggles de mute
        musicMuteToggle.isOn = !AudioManager.Instance._audioData.isMuteMusic;
        sfxMuteToggle.isOn = !AudioManager.Instance._audioData.isMuteSFX;

        // Asignar eventos
        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        musicMuteToggle.onValueChanged.AddListener(OnMusicMuteToggle);
        sfxMuteToggle.onValueChanged.AddListener(OnSFXMuteToggle);
    }

    private void OnMasterVolumeChanged(float value)
    {
        AudioManager.Instance.Setmaster(value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusic(value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFX(value);
    }

    private void OnMusicMuteToggle(bool isOn)
    {
        if (isOn)
            AudioManager.Instance.UnMuteMusic();
        else
            AudioManager.Instance.MuteMusic();
    }

    private void OnSFXMuteToggle(bool isOn)
    {
        if (isOn)
            AudioManager.Instance.UnMuteSFX();
        else
            AudioManager.Instance.MuteSFX();
    }
}
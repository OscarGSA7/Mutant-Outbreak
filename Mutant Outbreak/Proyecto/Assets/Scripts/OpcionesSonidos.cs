using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OpcionesSonido : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderSFX;

    private void Start()
    {
        
        float value;
        audioMixer.GetFloat("MasterVolume", out value);
        sliderMaster.value = value;

        audioMixer.GetFloat("MusicVolume", out value);
        sliderMusic.value = value;

        audioMixer.GetFloat("SFXVolume", out value);
        sliderSFX.value = value;

        
        sliderMaster.onValueChanged.AddListener(SetMasterVolume);
        sliderMusic.onValueChanged.AddListener(SetMusicVolume);
        sliderSFX.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }
}

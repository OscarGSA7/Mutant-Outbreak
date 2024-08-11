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
        audioMixer.GetFloat("Master", out value);
        sliderMaster.value = value;

        audioMixer.GetFloat("Music", out value);
        sliderMusic.value = value;

        audioMixer.GetFloat("Fx", out value);
        sliderSFX.value = value;

        sliderMaster.onValueChanged.AddListener(SetMasterVolume);
        sliderMusic.onValueChanged.AddListener(SetMusicVolume);
        sliderSFX.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("Fx", volume);
    }
}

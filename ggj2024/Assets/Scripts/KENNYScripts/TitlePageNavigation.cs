using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class TitlePageNavigation : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _titleOptions;

    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;



    private void Awake()
    {
        AudioPreference();
    }

    public void MasterVolume()
    {
        _audioMixer.SetFloat("Master", _masterSlider.value);
    }

    public void MusicVolume()
    {
        _audioMixer.SetFloat("Music", _musicSlider.value);
    }

    public void SFXVolume()
    {
        _audioMixer.SetFloat("SFX", _SFXSlider.value);
    }

    public void AudioPreference()
    {
        PlayerPrefs.GetFloat("Master");
        PlayerPrefs.GetFloat("Music");
        PlayerPrefs.GetFloat("SFX");
    }

    public void OpenSettings()
    {
        _titleOptions.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        PlayerPrefs.Save();
        _settingsMenu.SetActive(false);
        _titleOptions.SetActive(true);

    }
}


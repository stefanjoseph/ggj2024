using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingsMenu;

    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;

    private bool _menuIsActive;

    private void Awake()
    {
        AudioPreference();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TurnOffMenu();
        }
    }

    public void TurnOffMenu()
    {
        _menuIsActive = !_menuIsActive;

        if (_menuIsActive == true)
        {
            Time.timeScale = 0f;
            SwitchMenu();
        }

        if (_menuIsActive == false)
        {
            Time.timeScale = 1.0f;
            SwitchMenu();
        }
    }

    public void SwitchMenu()
    {
        if (_menuIsActive == true)
        {
            _pauseMenu.SetActive(true);
        }

        if (_menuIsActive == false)
        {
            _pauseMenu.SetActive(false);
        }
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

    public void CloseSettings()
    {
        PlayerPrefs.Save();
        _settingsMenu.SetActive(false);
    }
}

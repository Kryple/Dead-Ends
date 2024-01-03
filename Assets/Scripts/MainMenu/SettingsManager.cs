using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _masterSlider;
    
    private string p_musicVolume = "Music Volume";
    private string p_sFXVolume = "SFX Volume";
    private string p_masterVolume = "Master Volume";


    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        
        //AudioMixer's volume changes logarithmically, but the value of the slider changes linearly 
        //why 20?
        //Go back to Unity, and set value of the slider: min = 0.0001, max = 1
        _musicSlider.minValue = -40;
        _musicSlider.maxValue = 0;
        // _audioMixer.SetFloat(p_musicVolume, Mathf.Log(volume) * 20);
        _audioMixer.SetFloat(p_musicVolume, volume);
    }

    public void SetMasterVolume()
    {
        float volume = _masterSlider.value;
        
        //AudioMixer's volume changes logarithmically, but the value of the slider changes linearly 
        //why 20?
        //Go back to Unity, and set value of the slider: min = 0.0001, max = 1
        _musicSlider.minValue = -40;
        _musicSlider.maxValue = 0;
        // _audioMixer.SetFloat(p_musicVolume, Mathf.Log(volume) * 20);
        _audioMixer.SetFloat(p_masterVolume, volume);
    }

    public void SetSFXVolume()
    {
        float volume = _SFXSlider.value;
        
        //AudioMixer's volume changes logarithmically, but the value of the slider changes linearly 
        //why 20?
        //Go back to Unity, and set value of the slider: min = 0.0001, max = 1
        _musicSlider.minValue = -40;
        _musicSlider.maxValue = 0;
        // _audioMixer.SetFloat(p_musicVolume, Mathf.Log(volume) * 20);
        _audioMixer.SetFloat(p_sFXVolume, volume);
    }
}

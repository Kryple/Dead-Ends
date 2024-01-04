using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
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

    private void Start()
    {
        Debug.Log("music: " + PlayerPrefs.GetFloat(p_musicVolume));
        Debug.Log("master: " + PlayerPrefs.GetFloat(p_masterVolume));
        Debug.Log("sfx: " + PlayerPrefs.GetFloat(p_sFXVolume));
        
        _musicSlider.minValue = -40;
        _musicSlider.maxValue = 0;
        
        _SFXSlider.minValue = -40;
        _SFXSlider.maxValue = 0;
        
        _masterSlider.minValue = -40;
        _masterSlider.maxValue = 0;
        
        LoadVolume();
    }

    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        
        //AudioMixer's volume changes logarithmically, but the value of the slider changes linearly 
        //why 20?
        //Go back to Unity, and set value of the slider: min = 0.0001, max = 1
        
        // _audioMixer.SetFloat(p_musicVolume, Mathf.Log(volume) * 20);
        _audioMixer.SetFloat(p_musicVolume, volume);
        PlayerPrefs.SetFloat(p_musicVolume, volume);
        
        Debug.Log("mody music: " + PlayerPrefs.GetFloat(p_musicVolume));
    }

    public void SetMasterVolume()
    {
        float volume = _masterSlider.value;
        
        
        _audioMixer.SetFloat(p_masterVolume, volume);
        PlayerPrefs.SetFloat(p_masterVolume, volume);
        
        Debug.Log("mody master: " + PlayerPrefs.GetFloat(p_masterVolume));
    }

    public void SetSFXVolume()
    {
        float volume = _SFXSlider.value;
        Debug.Log("mody sfx: " + volume);
        
        _audioMixer.SetFloat(p_sFXVolume, volume);
        PlayerPrefs.SetFloat(p_sFXVolume, volume);
        
        
    }

    public void LoadVolume()
    {
        //
        _musicSlider.value = PlayerPrefs.GetFloat(p_musicVolume, 0);
        Debug.Log("music volume: " + PlayerPrefs.GetFloat(p_musicVolume));
        Debug.Log("music slider: " + _musicSlider.value);
        _SFXSlider.value = PlayerPrefs.GetFloat(p_sFXVolume, 0);
        _masterSlider.value = PlayerPrefs.GetFloat(p_masterVolume, 0);
        
        
        
    }
}

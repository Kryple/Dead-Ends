using System;
using System.Collections;
using System.Collections.Generic;
using Core.Observer_Pattern;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour, IObserver
{
    
    
    
    [SerializeField] private GameObject _musicManager;

    [SerializeField] private GameObject _sFXManager;

    [SerializeField] private AudioClip _mainMenuBackground;
    [SerializeField] private AudioClip _inGameBackground;

    private AudioSource _musicSource;

    //Singleton Pattern
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

        _musicSource = _musicManager.GetComponent<AudioSource>();
        _musicSource.clip = _mainMenuBackground;
        _musicSource.volume = 0.65f;
        _musicSource.pitch = 0.7f;
        _musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //This code still hasn't run
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded");
        _musicSource.Stop();

        switch (scene.buildIndex)
        {
            case 0:
                _musicSource.clip = _mainMenuBackground;
                break;
            case 1:
                _musicSource.clip = _inGameBackground;
                break;
            
        }
        
        _musicSource.Play();

    }
}

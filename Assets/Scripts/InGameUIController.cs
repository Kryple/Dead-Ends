using System;
using System.Collections;
using System.Collections.Generic;
using Core.Observer_Pattern;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InGameUIController : MonoBehaviour, IObserver
{
    [SerializeField] private List<GameObject> _playerLivesList;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _backgroundSound;
    private GameObject _player;
    private int _liveId = 2;


    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _highScore;
  

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        foreach (GameObject live in _playerLivesList)
        {
            live.SetActive(true);
        }

        _audioSource.clip = _backgroundSound;
        _audioSource.pitch = 0.7f;
        _audioSource.volume = 0.45f;
        _audioSource.Play();
        _audioSource.loop = true;
    }
    
    
    public void OnNotify(IEvent @event)
    {
        switch (@event)
        {
            case IEvent.OnPlayerDie:
                _gameOverScreen.SetActive(true);
                _highScore.GetComponent<TextMeshProUGUI>().SetText($"High Score = {PlayerPrefs.GetInt("HighScore")}");
                break;
            
            case IEvent.OnPlayerGetHurt:
                Debug.Log("Player hurt");
                _playerLivesList[_liveId--].SetActive(false);
                break;
        }
    }

}